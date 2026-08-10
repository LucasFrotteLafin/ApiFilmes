<script lang="ts">
import MovieCard from '../components/MovieCard.vue'
import MovieCarousel from '../components/MovieCarousel.vue'
import api from '../api/axios'

interface Movie {
  id: number
  title: string
  posterUrl: string
  overview: string
}

export default {
  name: 'MoviesView',
  components: { MovieCard, MovieCarousel },
  data() {
    return {
      movies: [] as Movie[],
      carregando: true,
      erro: '',
    }
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
      <section class="movies-page__carousel-section">
        <h2 class="movies-page__section-title">Em Destaque</h2>
        <MovieCarousel :movies="movies" />
      </section>

      <section class="movies-page__grid-section">
        <h2 class="movies-page__section-title">Todos os Filmes</h2>
        <p v-if="movies.length === 0" class="movies-page__status">Nenhum filme cadastrado ainda.</p>
        <div v-else class="movies-page__grid">
          <MovieCard v-for="movie in movies" :key="movie.id" :movie="movie" />
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

.movies-page__status--erro {
  color: #e50914;
}

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

.movies-page__grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
  gap: 24px;
}
</style>
