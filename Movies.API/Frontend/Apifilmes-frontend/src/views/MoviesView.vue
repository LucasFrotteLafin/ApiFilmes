<script lang="ts">
import MovieCard from '../components/MovieCard.vue'
import MovieCarousel from '../components/MovieCarousel.vue'
import movies from '../data/movies'

export default {
  name: 'MoviesView',
  components: { MovieCard, MovieCarousel },
  data() {
    return { movies }
  },
}
</script>

<template>
  <div class="movies-page">
    <section class="movies-page__carousel-section">
      <h2 class="movies-page__section-title">Em Destaque</h2>
      <MovieCarousel :movies="movies" />
    </section>

    <section class="movies-page__grid-section">
      <h2 class="movies-page__section-title">Todos os Filmes</h2>
      <div class="movies-page__grid">
        <MovieCard v-for="movie in movies" :key="movie.id" :movie="movie" />
      </div>
    </section>
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

.movies-page__section-title {
  color: #fff;
  font-size: 1.4rem;
  margin: 0 0 28px;
  padding-left: 4px;
  border-left: 4px solid #e50914;
  padding-left: 12px;
}

.movies-page__carousel-section {
  display: flex;
  flex-direction: column;
}

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
