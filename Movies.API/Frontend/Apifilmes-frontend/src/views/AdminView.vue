<script lang="ts">
import api from '../api/axios'

interface Movie {
  id: number
  title: string
  posterUrl: string
  overview: string
}

interface Form {
  title: string
  posterUrl: string
  overview: string
}

export default {
  name: 'AdminView',
  data() {
    return {
      movies: [] as Movie[],
      dialog: false,
      editingId: null as number | null,
      form: { title: '', posterUrl: '', overview: '' } as Form,
      carregando: false,
      erro: '',
      sucesso: '',
    }
  },
  async mounted() {
    await this.carregarFilmes()
  },
  methods: {
    async carregarFilmes() {
      const res = await api.get('/movie/get-all')
      this.movies = res.data
    },
    abrirCriar() {
      this.editingId = null
      this.form = { title: '', posterUrl: '', overview: '' }
      this.erro = ''
      this.sucesso = ''
      this.dialog = true
    },
    abrirEditar(movie: Movie) {
      this.editingId = movie.id
      this.form = { title: movie.title, posterUrl: movie.posterUrl, overview: movie.overview }
      this.erro = ''
      this.sucesso = ''
      this.dialog = true
    },
    async salvar() {
      this.erro = ''
      this.sucesso = ''
      this.carregando = true
      try {
        if (this.editingId) {
          await api.put(`/movie/${this.editingId}`, this.form)
          this.sucesso = 'Filme atualizado!'
        } else {
          await api.post('/movie', this.form)
          this.sucesso = 'Filme criado!'
        }
        await this.carregarFilmes()
        setTimeout(() => { this.dialog = false }, 1000)
      } catch {
        this.erro = 'Erro ao salvar filme.'
      } finally {
        this.carregando = false
      }
    },
  },
}
</script>

<template>
  <div class="admin-page">
    <div class="admin-header">
      <h1 class="admin-title">Painel Admin</h1>
      <v-btn color="primary" @click="abrirCriar">+ Novo Filme</v-btn>
    </div>

    <div class="admin-grid">
      <div v-for="movie in movies" :key="movie.id" class="admin-card">
        <img :src="movie.posterUrl" :alt="movie.title" class="admin-card__poster" />
        <div class="admin-card__info">
          <p class="admin-card__title">{{ movie.title }}</p>
          <v-btn size="small" color="primary" variant="outlined" @click="abrirEditar(movie)">Editar</v-btn>
        </div>
      </div>
    </div>

    <v-dialog v-model="dialog" max-width="500">
      <v-card color="#1a1a1a">
        <v-card-title class="dialog-title">{{ editingId ? 'Editar Filme' : 'Novo Filme' }}</v-card-title>
        <v-card-text>
          <v-form @submit.prevent="salvar">
            <v-text-field v-model="form.title" label="Título" variant="underlined" color="primary" class="mb-2" />
            <v-text-field v-model="form.posterUrl" label="URL do Poster" variant="underlined" color="primary" class="mb-2" />
            <v-textarea v-model="form.overview" label="Descrição" variant="underlined" color="primary" rows="3" class="mb-2" />
            <p v-if="erro" class="msg-erro">{{ erro }}</p>
            <p v-if="sucesso" class="msg-sucesso">{{ sucesso }}</p>
            <div class="dialog-actions">
              <v-btn variant="text" color="grey" @click="dialog = false">Cancelar</v-btn>
              <v-btn type="submit" color="primary" :loading="carregando">Salvar</v-btn>
            </div>
          </v-form>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<style scoped>
.admin-page {
  min-height: calc(100vh - 60px);
  background: #141414;
  padding: 40px 32px;
}

.admin-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 32px;
}

.admin-title {
  color: #fff;
  font-size: 1.6rem;
  font-weight: 700;
  border-left: 4px solid #e50914;
  padding-left: 12px;
}

.admin-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(160px, 1fr));
  gap: 20px;
}

.admin-card {
  background: #1a1a1a;
  border-radius: 8px;
  overflow: hidden;
}

.admin-card__poster {
  width: 100%;
  aspect-ratio: 2/3;
  object-fit: cover;
  display: block;
}

.admin-card__info {
  padding: 8px 10px;
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.admin-card__title {
  color: #fff;
  font-size: 0.85rem;
  font-weight: 600;
  margin: 0;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.dialog-title {
  color: #fff;
  font-size: 1.2rem;
  font-weight: 700;
  padding-top: 20px;
}

.dialog-actions {
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  margin-top: 8px;
}

.msg-erro {
  color: #e50914;
  font-size: 0.875rem;
  margin-bottom: 8px;
}

.msg-sucesso {
  color: #4caf50;
  font-size: 0.875rem;
  margin-bottom: 8px;
}
</style>
