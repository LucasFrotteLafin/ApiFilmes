<script lang="ts">
import type { Movie } from '../data/movies'

export default {
  props: {
    movie: { type: Object as () => Movie, required: true },
  },
}
</script>

<template>
  <div class="movie-card">
    <div class="movie-card__poster-wrap">
      <img :src="movie.poster" :alt="movie.title" class="movie-card__poster" />
      <div class="movie-card__overlay">
        <p class="movie-card__description">{{ movie.description }}</p>
      </div>
    </div>
    <div class="movie-card__info">
      <h3 class="movie-card__title">{{ movie.title }}</h3>
    </div>
  </div>
</template>

<style scoped>
.movie-card {
  border-radius: 10px;
  overflow: hidden;
  background: #1a1a1a;
  transition: transform 0.25s, box-shadow 0.25s;
  cursor: pointer;
}

.movie-card:hover {
  transform: translateY(-6px);
  box-shadow: 0 12px 32px rgba(229, 9, 20, 0.35);
}

.movie-card__poster-wrap {
  position: relative;
  overflow: hidden;
}

.movie-card__poster {
  width: 100%;
  aspect-ratio: 2/3;
  object-fit: cover;
  display: block;
  transition: transform 0.35s;
}

.movie-card:hover .movie-card__poster {
  transform: scale(1.05);
}

.movie-card__overlay {
  position: absolute;
  inset: 0;
  background: linear-gradient(to top, rgba(0,0,0,0.92) 40%, transparent);
  opacity: 0;
  transition: opacity 0.3s;
  display: flex;
  align-items: flex-end;
  padding: 16px;
}

.movie-card:hover .movie-card__overlay {
  opacity: 1;
}

.movie-card__description {
  font-size: 0.78rem;
  color: #ddd;
  line-height: 1.5;
  margin: 0;
  display: -webkit-box;
  -webkit-line-clamp: 4;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.movie-card__info {
  padding: 10px 12px;
}

.movie-card__title {
  font-size: 0.95rem;
  font-weight: 700;
  color: #fff;
  margin: 0;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
