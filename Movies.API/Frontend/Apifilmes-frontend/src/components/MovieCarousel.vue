<script lang="ts">
import type { Movie } from '../types/Movie'
export default {
  name: 'MovieCarousel',
  props: {
    movies: { type: Array as () => Movie[], required: true },
  },
  data() {
    return {
      current: 0,
      timer: null as ReturnType<typeof setInterval> | null,
    }
  },
  computed: {
    prev(): number {
      return (this.current - 1 + this.movies.length) % this.movies.length
    },
    next(): number {
      return (this.current + 1) % this.movies.length
    },
    activeMovie(): Movie {
      return this.movies[this.current]
    },
  },
  mounted() {
    this.timer = setInterval(() => { this.current = this.next }, 4000)
  },
  beforeUnmount() {
    if (this.timer) clearInterval(this.timer)
  },
  methods: {
    goPrev() { this.current = this.prev },
    goNext() { this.current = this.next },
    goTo(index: number) { this.current = index },
  },
}
</script>
<template>
  <div v-if="movies.length > 0" class="carousel">
    <div
      class="carousel__bg"
      :style="{ backgroundImage: `url(${activeMovie.posterUrl})` }"
    />
    <div class="carousel__content">
      <button class="carousel__arrow" @click="goPrev">&#8249;</button>
      <div class="carousel__track">
        <div
          v-for="(movie, index) in movies"
          :key="movie.id"
          class="carousel__item"
          :class="{
            'carousel__item--active': index === current,
            'carousel__item--prev': index === prev,
            'carousel__item--next': index === next,
          }"
          @click="goTo(index)"
        >
          <img :src="movie.posterUrl" :alt="movie.title" class="carousel__poster" />
        </div>
      </div>
      <button class="carousel__arrow" @click="goNext">&#8250;</button>
    </div>
    <div class="carousel__info">
      <h3 class="carousel__title">{{ activeMovie.title }}</h3>
      <p class="carousel__desc">{{ activeMovie.overview }}</p>
      <div class="carousel__dots">
        <button
          v-for="(_, i) in movies"
          :key="i"
          class="carousel__dot"
          :class="{ 'carousel__dot--active': i === current }"
          @click="goTo(i)"
        />
      </div>
    </div>
  </div>
</template>
<style scoped>
.carousel {
  position: relative;
  border-radius: 16px;
  overflow: hidden;
  padding: 48px 0 36px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 24px;
}
.carousel__bg {
  position: absolute;
  inset: 0;
  background-size: cover;
  background-position: center;
  filter: blur(28px) brightness(0.3);
  transform: scale(1.1);
  transition: background-image 0.5s ease;
}
.carousel__content {
  position: relative;
  display: flex;
  align-items: center;
  gap: 16px;
  width: 100%;
  justify-content: center;
}
.carousel__track {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 360px;
  position: relative;
  width: 640px;
}
.carousel__item {
  position: absolute;
  transition: all 0.45s cubic-bezier(0.4, 0, 0.2, 1);
  opacity: 0;
  transform: scale(0.6);
  pointer-events: none;
  cursor: pointer;
}
.carousel__item--prev {
  opacity: 0.45;
  transform: scale(0.78) translateX(-180px);
  pointer-events: auto;
}
.carousel__item--next {
  opacity: 0.45;
  transform: scale(0.78) translateX(180px);
  pointer-events: auto;
}
.carousel__item--active {
  opacity: 1;
  transform: scale(1) translateX(0);
  pointer-events: auto;
  z-index: 2;
}
.carousel__poster {
  height: 320px;
  border-radius: 12px;
  box-shadow: 0 16px 48px rgba(0, 0, 0, 0.8);
  display: block;
}
.carousel__item--active .carousel__poster {
  box-shadow: 0 20px 60px rgba(229, 9, 20, 0.4);
}
.carousel__arrow {
  position: relative;
  z-index: 3;
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(4px);
  border: 1px solid rgba(255, 255, 255, 0.15);
  color: #fff;
  font-size: 2rem;
  width: 44px;
  height: 44px;
  border-radius: 50%;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background 0.2s;
  flex-shrink: 0;
}
.carousel__arrow:hover {
  background: #e50914;
  border-color: #e50914;
}
.carousel__info {
  position: relative;
  text-align: center;
  max-width: 500px;
  padding: 0 16px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
}
.carousel__title {
  font-size: 1.5rem;
  font-weight: 700;
  color: #fff;
  margin: 0;
}
.carousel__desc {
  font-size: 0.875rem;
  color: #bbb;
  line-height: 1.6;
  margin: 0;
}
.carousel__dots {
  display: flex;
  gap: 8px;
  margin-top: 4px;
}
.carousel__dot {
  width: 8px;
  height: 8px;
  border-radius: 50%;
  border: none;
  background: rgba(255, 255, 255, 0.3);
  cursor: pointer;
  transition: background 0.2s, transform 0.2s;
  padding: 0;
}
.carousel__dot--active {
  background: #e50914;
  transform: scale(1.3);
}
</style>