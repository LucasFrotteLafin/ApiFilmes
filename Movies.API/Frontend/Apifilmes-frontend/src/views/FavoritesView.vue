<script lang="ts">
import MovieCard from '../components/MovieCard.vue'
import api from '../api/axios'
import type { Movie } from '../types/Movie'

export default {
  name: 'FavoritesView',
  components: { MovieCard },
  data() {
    return {
      movies: [] as Movie[],
      carregando: true,
      erro: '',
    }
  },
  computed: {
    isAdmin() {
      return this.$store.getters.isAdmin
    },
    favoriteIds(): number[] {
      return this.$store.getters.favorites
    },
    favoriteMovies(): Movie[] {
      return this.movies.filter(m => this.favoriteIds.includes(m.id))
    },
  },
  methods: {
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
  <div class="favorites-page">
    <div class="favorites-page__header">
      <h1 class="favorites-page__title">❤️ Meus Favoritos</h1>
      <p class="favorites-page__subtitle">Filmes que você marcou como favoritos</p>
    </div>

    <div v-if="carregando" class="favorites-page__status">Carregando...</div>
    <div v-else-if="erro" class="favorites-page__status favorites-page__status--erro">{{ erro }}</div>
    <div v-else-if="favoriteMovies.length === 0" class="favorites-page__empty">
      <p class="favorites-page__empty-icon">🎬</p>
      <p class="favorites-page__empty-text">Você ainda não tem filmes favoritos</p>
      <router-link to="/filmes" class="favorites-page__empty-btn">Explorar Filmes</router-link>
    </div>
    <div v-else class="favorites-page__grid">
      <MovieCard 
        v-for="movie in favoriteMovies" 
        :key="movie.id" 
        :movie="movie" 
        :isAdmin="isAdmin"
        @deletar="deletar" 
      />
    </div>
  </div>
</template>

<style scoped>
.favorites-page {
  min-height: calc(100vh - 60px);
  background: #141414;
  padding: 48px 32px;
}

.favorites-page__header {
  margin-bottom: 48px;
  text-align: center;
}

.favorites-page__title {
  color: #fff;
  font-size: 2.5rem;
  margin: 0 0 12px;
  font-weight: 700;
}

.favorites-page__subtitle {
  color: #aaa;
  font-size: 1.1rem;
  margin: 0;
}

.favorites-page__status {
  color: #aaa;
  text-align: center;
  font-size: 1rem;
  padding: 60px 0;
}

.favorites-page__status--erro {
  color: #e50914;
}

.favorites-page__empty {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 80px 20px;
  text-align: center;
}

.favorites-page__empty-icon {
  font-size: 4rem;
  margin: 0 0 20px;
}

.favorites-page__empty-text {
  color: #aaa;
  font-size: 1.2rem;
  margin: 0 0 24px;
}

.favorites-page__empty-btn {
  background: #e50914;
  color: #fff;
  padding: 12px 32px;
  border-radius: 8px;
  text-decoration: none;
  font-weight: 600;
  transition: background 0.2s;
}

.favorites-page__empty-btn:hover {
  background: #c40812;
}

.favorites-page__grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
  gap: 24px;
}
</style>
