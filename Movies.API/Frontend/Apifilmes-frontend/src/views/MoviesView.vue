<script lang="ts">
import MovieCard from '../components/MovieCard.vue'
import MovieCarousel from '../components/MovieCarousel.vue'
import api from '../api/axios'
import type { Movie } from '../types/Movie'

const GENRES = ['Ação', 'Aventura', 'Comédia', 'Drama', 'Terror', 'Ficção Científica', 'Romance', 'Animação', 'Documentário', 'Thriller']

export default {
  name: 'MoviesView',
  components: { MovieCard, MovieCarousel },
  data() {
    return {
      movies: [] as Movie[],
      carregando: true,
      erro: '',
      dialog: false,
      editingId: null as number | null,
      form: { title: '', posterUrl: '', overview: '', genre: '', rating: 0, trailerUrl: '' },
      salvando: false,
      erroForm: '',
      sucessoForm: '',
      busca: '',
      generoFiltro: '',
      genres: GENRES,
    }
  },
  computed: {
    isAdmin() {
      return this.$store.getters.isAdmin
    },
    filmesFiltrados(): Movie[] {
      return this.movies.filter(m => {
        const matchBusca = m.title.toLowerCase().includes(this.busca.toLowerCase())
        const matchGenero = this.generoFiltro ? m.genre === this.generoFiltro : true
        return matchBusca && matchGenero
      })
    },
  },
  methods: {
    abrirCriar() {
      this.editingId = null
      this.form = { title: '', posterUrl: '', overview: '', genre: '', rating: 0, trailerUrl: '' }
      this.erroForm = ''
      this.sucessoForm = ''
      this.dialog = true
    },
    abrirEditar(movie: Movie) {
      this.editingId = movie.id
      this.form = { title: movie.title, posterUrl: movie.posterUrl, overview: movie.overview, genre: movie.genre, rating: movie.rating, trailerUrl: movie.trailerUrl || '' }
      this.erroForm = ''
      this.sucessoForm = ''
      this.dialog = true
    },
    async deletar(movie: Movie) {
      if (!confirm(`Deletar "${movie.title}"?`)) return
      try {
        await api.delete(`/movie/${movie.id}`)
        const resposta = await api.get('/movie/get-all')
        this.movies = resposta.data
      } catch {
        alert('Erro ao deletar filme.')
      }
    },
    async salvar() {
      this.erroForm = ''
      this.sucessoForm = ''
      this.salvando = true
      try {
        if (this.editingId) {
          await api.put(`/movie/${this.editingId}`, this.form)
          this.sucessoForm = 'Filme atualizado!'
        } else {
          await api.post('/movie', this.form)
          this.sucessoForm = 'Filme criado!'
        }
        const resposta = await api.get('/movie/get-all')
        this.movies = resposta.data
        setTimeout(() => {
          this.dialog = false
          this.editingId = null
          this.form = { title: '', posterUrl: '', overview: '', genre: '', rating: 0, trailerUrl: '' }
          this.sucessoForm = ''
        }, 1200)
      } catch {
        this.erroForm = 'Erro ao salvar filme.'
      } finally {
        this.salvando = false
      }
    },
  },
  async mounted() {
    try {
      const resposta = await api.get('/movie/get-all')
      this.movies = resposta.data
    } catch {
      this.erro = 'Não foi possível carregar os filmes.'
    } finally {
      this.carregando = false
    }
  },
}
</script>

<template>
  <div class="movies-page">
    <div v-if="carregando" class="movies-page__status">Carregando filmes...</div>
    <div v-else-if="erro" class="movies-page__status movies-page__status--erro">{{ erro }}</div>

    <template v-else>
      <button v-if="isAdmin" class="movies-page__fab" @click="abrirCriar">+ Novo Filme</button>

      <v-dialog v-model="dialog" max-width="560">
        <v-card class="movie-dialog">
          <div class="movie-dialog__header">
            <span class="movie-dialog__header-bar"></span>
            <h2 class="movie-dialog__title">{{ editingId ? 'Editar Filme' : 'Adicionar Filme' }}</h2>
            <button class="movie-dialog__close" @click="dialog = false">✕</button>
          </div>

          <div class="movie-dialog__body">
            <div class="movie-dialog__preview" :style="form.posterUrl ? `background-image:url(${form.posterUrl})` : ''">
              <div class="movie-dialog__preview-overlay">
                <span v-if="!form.posterUrl" class="movie-dialog__preview-placeholder">Pré-visualização do Poster</span>
                <span v-else class="movie-dialog__preview-title">{{ form.title || 'Título do Filme' }}</span>
              </div>
            </div>

            <v-form class="movie-dialog__form" @submit.prevent="salvar">
              <div class="movie-dialog__field">
                <label class="movie-dialog__label">Título <span class="movie-dialog__required">*</span></label>
                <input v-model="form.title" class="movie-dialog__input" placeholder="Ex: Inception" required />
              </div>

              <div class="movie-dialog__field">
                <label class="movie-dialog__label">URL do Poster <span class="movie-dialog__required">*</span></label>
                <input v-model="form.posterUrl" class="movie-dialog__input" placeholder="https://..." required />
              </div>

              <div class="movie-dialog__field">
                <label class="movie-dialog__label">Gênero <span class="movie-dialog__required">*</span></label>
                <select v-model="form.genre" class="movie-dialog__input" required>
                  <option value="" disabled>Selecione um gênero</option>
                  <option v-for="g in genres" :key="g" :value="g">{{ g }}</option>
                </select>
              </div>

              <div class="movie-dialog__field">
                <label class="movie-dialog__label">Avaliação ({{ form.rating }}/5)</label>
                <div class="movie-dialog__stars">
                  <button
                    v-for="n in 5" :key="n"
                    type="button"
                    class="movie-dialog__star"
                    :class="{ 'movie-dialog__star--active': n <= form.rating }"
                    @click="form.rating = n"
                  >★</button>
                </div>
              </div>

              <div class="movie-dialog__field">
                <label class="movie-dialog__label">URL do Trailer (YouTube Embed ou Vimeo)</label>
                <input v-model="form.trailerUrl" class="movie-dialog__input" placeholder="https://www.youtube.com/embed/..." />
              </div>

              <div class="movie-dialog__field">
                <label class="movie-dialog__label">Descrição <span class="movie-dialog__required">*</span></label>
                <textarea v-model="form.overview" class="movie-dialog__textarea" placeholder="Sinopse do filme..." rows="3" required></textarea>
              </div>

              <p v-if="erroForm" class="movie-dialog__msg movie-dialog__msg--erro">{{ erroForm }}</p>
              <p v-if="sucessoForm" class="movie-dialog__msg movie-dialog__msg--sucesso">✓ {{ sucessoForm }}</p>

              <div class="movie-dialog__actions">
                <button type="button" class="movie-dialog__btn movie-dialog__btn--cancel" @click="dialog = false">Cancelar</button>
                <button type="submit" class="movie-dialog__btn movie-dialog__btn--save" :disabled="salvando">
                  {{ salvando ? 'Salvando...' : editingId ? 'Salvar Alterações' : 'Publicar Filme' }}
                </button>
              </div>
            </v-form>
          </div>
        </v-card>
      </v-dialog>

      <section class="movies-page__carousel-section">
        <h2 class="movies-page__section-title">Em Destaque</h2>
        <MovieCarousel :movies="movies" />
      </section>

      <section class="movies-page__grid-section">
        <div class="movies-page__filters">
          <input v-model="busca" class="movies-page__search" placeholder="🔍 Buscar filme..." />
          <div class="movies-page__genres">
            <button
              class="movies-page__genre-btn"
              :class="{ 'movies-page__genre-btn--active': generoFiltro === '' }"
              @click="generoFiltro = ''"
            >Todos</button>
            <button
              v-for="g in genres" :key="g"
              class="movies-page__genre-btn"
              :class="{ 'movies-page__genre-btn--active': generoFiltro === g }"
              @click="generoFiltro = g"
            >{{ g }}</button>
          </div>
        </div>

        <h2 class="movies-page__section-title">Todos os Filmes</h2>
        <p v-if="filmesFiltrados.length === 0" class="movies-page__status">Nenhum filme encontrado.</p>
        <div v-else class="movies-page__grid">
          <MovieCard v-for="movie in filmesFiltrados" :key="movie.id" :movie="movie" :isAdmin="isAdmin" @editar="abrirEditar" @deletar="deletar" />
        </div>
      </section>
    </template>
  </div>
</template>

<style scoped>
.movies-page {
  min-height: calc(100vh - 60px);
  background: #141414;
  padding: 48px 32px;
  display: flex;
  flex-direction: column;
  gap: 64px;
}

.movies-page__status {
  color: #aaa;
  text-align: center;
  font-size: 1rem;
  padding: 60px 0;
}

.movies-page__status--erro { color: #e50914; }

.movies-page__section-title {
  color: #fff;
  font-size: 1.4rem;
  margin: 0 0 28px;
  border-left: 4px solid #e50914;
  padding-left: 12px;
}

.movies-page__carousel-section,
.movies-page__grid-section {
  display: flex;
  flex-direction: column;
}

.movies-page__filters {
  display: flex;
  flex-direction: column;
  gap: 16px;
  margin-bottom: 28px;
}

.movies-page__search {
  background: #1a1a1a;
  border: 1px solid #333;
  border-radius: 10px;
  color: #fff;
  font-size: 0.95rem;
  padding: 12px 16px;
  outline: none;
  max-width: 400px;
  transition: border-color 0.2s;
}

.movies-page__search:focus { border-color: #e50914; }
.movies-page__search::placeholder { color: #555; }

.movies-page__genres {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
}

.movies-page__genre-btn {
  background: #1a1a1a;
  border: 1px solid #333;
  color: #aaa;
  font-size: 0.8rem;
  padding: 6px 14px;
  border-radius: 20px;
  cursor: pointer;
  transition: all 0.2s;
}

.movies-page__genre-btn:hover { border-color: #e50914; color: #fff; }

.movies-page__genre-btn--active {
  background: #e50914;
  border-color: #e50914;
  color: #fff;
}

.movies-page__grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
  gap: 24px;
}

.movies-page__fab {
  position: fixed;
  bottom: 32px;
  right: 32px;
  background: #e50914;
  color: #fff;
  border: none;
  padding: 14px 24px;
  border-radius: 32px;
  font-size: 1rem;
  font-weight: 700;
  cursor: pointer;
  box-shadow: 0 4px 16px rgba(229,9,20,0.4);
  z-index: 200;
  transition: background 0.2s;
}

.movies-page__fab:hover { background: #c40812; }

.movie-dialog {
  background: #1a1a1a !important;
  border-radius: 16px !important;
  overflow: hidden;
  padding: 0 !important;
}

.movie-dialog__header {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 20px 24px 16px;
  border-bottom: 1px solid #2a2a2a;
}

.movie-dialog__header-bar {
  width: 4px;
  height: 22px;
  background: #e50914;
  border-radius: 4px;
  flex-shrink: 0;
}

.movie-dialog__title {
  color: #fff;
  font-size: 1.2rem;
  font-weight: 700;
  margin: 0;
  flex: 1;
}

.movie-dialog__close {
  background: none;
  border: none;
  color: #888;
  font-size: 1.1rem;
  cursor: pointer;
  padding: 4px 8px;
  border-radius: 6px;
  transition: color 0.2s;
}

.movie-dialog__close:hover { color: #fff; }

.movie-dialog__body {
  display: flex;
  gap: 20px;
  padding: 20px 24px 24px;
  max-height: 75vh;
  overflow-y: auto;
}

.movie-dialog__preview {
  width: 120px;
  flex-shrink: 0;
  border-radius: 10px;
  background: #111;
  background-size: cover;
  background-position: center;
  aspect-ratio: 2/3;
  overflow: hidden;
  position: relative;
  align-self: flex-start;
}

.movie-dialog__preview-overlay {
  position: absolute;
  inset: 0;
  background: linear-gradient(to top, rgba(0,0,0,0.85) 40%, transparent);
  display: flex;
  align-items: flex-end;
  padding: 10px;
}

.movie-dialog__preview-placeholder { color: #555; font-size: 0.7rem; text-align: center; width: 100%; }
.movie-dialog__preview-title { color: #fff; font-size: 0.75rem; font-weight: 600; overflow: hidden; }

.movie-dialog__form {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.movie-dialog__field { display: flex; flex-direction: column; gap: 6px; }

.movie-dialog__label {
  color: #aaa;
  font-size: 0.8rem;
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 0.05em;
}

.movie-dialog__required { color: #e50914; }

.movie-dialog__input,
.movie-dialog__textarea {
  background: #111;
  border: 1px solid #333;
  border-radius: 8px;
  color: #fff;
  font-size: 0.9rem;
  padding: 10px 12px;
  outline: none;
  transition: border-color 0.2s;
  font-family: inherit;
  resize: none;
}

.movie-dialog__input:focus,
.movie-dialog__textarea:focus { border-color: #e50914; }

.movie-dialog__input::placeholder,
.movie-dialog__textarea::placeholder { color: #555; }

.movie-dialog__stars {
  display: flex;
  gap: 4px;
}

.movie-dialog__star {
  background: none;
  border: none;
  font-size: 1.6rem;
  color: #444;
  cursor: pointer;
  transition: color 0.15s, transform 0.1s;
  padding: 0;
  line-height: 1;
}

.movie-dialog__star:hover,
.movie-dialog__star--active { color: #f5c518; }

.movie-dialog__star:active { transform: scale(1.2); }

.movie-dialog__msg { font-size: 0.85rem; margin: 0; }
.movie-dialog__msg--erro { color: #e50914; }
.movie-dialog__msg--sucesso { color: #4caf50; }

.movie-dialog__actions {
  display: flex;
  gap: 10px;
  justify-content: flex-end;
  margin-top: 4px;
}

.movie-dialog__btn {
  border: none;
  border-radius: 8px;
  font-size: 0.9rem;
  font-weight: 600;
  padding: 10px 20px;
  cursor: pointer;
  transition: background 0.2s;
}

.movie-dialog__btn--cancel { background: #2a2a2a; color: #aaa; }
.movie-dialog__btn--cancel:hover { background: #333; color: #fff; }
.movie-dialog__btn--save { background: #e50914; color: #fff; }
.movie-dialog__btn--save:hover:not(:disabled) { background: #c40812; }
.movie-dialog__btn--save:disabled { opacity: 0.6; cursor: not-allowed; }
</style>
