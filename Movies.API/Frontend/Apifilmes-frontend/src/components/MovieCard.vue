<script lang="ts">
import type { Movie } from '../types/Movie'

export default {
  props: {
    movie: { type: Object as () => Movie, required: true },
    isAdmin: { type: Boolean, default: false },
  },

  emits: ['editar', 'deletar'],

  data() {
    return {
      modalAberto: false,
    }
  },

  computed: {
    ehFavorito(): boolean {
      return this.$store.getters.isFavorite(this.movie.id)
    },

    urlTrailer(): string {
      const url = this.movie.trailerUrl
      if (!url) return ''

      const vimeo = url.match(/vimeo\.com\/(\d+)(?:\?.*)?/)
      if (vimeo) {
        const fullUrl = url
        const videoId = vimeo[1]
        const queryParams = fullUrl.includes('?') ? fullUrl.split('?')[1] : ''
        
        if (queryParams) {
          return `https://player.vimeo.com/video/${videoId}?${queryParams}`
        }
        return `https://player.vimeo.com/video/${videoId}`
      }

      return url
    },
  },

  methods: {
    toggleFavorito() {
      if (this.ehFavorito) {
        this.$store.commit('removeFavorite', this.movie.id)
      } else {
        this.$store.commit('addFavorite', this.movie.id)
      }
    },
  },
}
</script>

<template>
  <div class="card" @click="modalAberto = true">
    <div class="card-poster">
      <img :src="movie.posterUrl" :alt="movie.title" />

      <button class="btn-fav" @click.stop="toggleFavorito">
        {{ ehFavorito ? '❤️' : '🤍' }}
      </button>

      <button v-if="isAdmin" class="btn-editar" @click.stop="$emit('editar', movie)">
        ✏️ Editar
      </button>
    </div>

    <div class="card-info">
      <h3>{{ movie.title }}</h3>
      <div class="card-meta">
        <span v-if="movie.genre" class="genero">{{ movie.genre }}</span>
        <span v-if="movie.rating" class="nota">★ {{ movie.rating }}/5</span>
      </div>
    </div>
  </div>

  <Teleport to="body">
    <div v-if="modalAberto" class="modal-fundo" @click.self="modalAberto = false">
      <div class="modal">
        <img :src="movie.posterUrl" :alt="movie.title" class="modal-poster" />

        <div class="modal-conteudo">
          <button class="btn-fechar" @click="modalAberto = false">✕</button>

          <h2>{{ movie.title }}</h2>

          <div class="modal-meta">
            <span v-if="movie.genre" class="genero">{{ movie.genre }}</span>
            <span v-if="movie.rating" class="nota">
              <span v-for="n in 5" :key="n" :style="{ color: n <= movie.rating ? '#f5c518' : '#555' }">★</span>
              {{ movie.rating }}/5
            </span>
          </div>

          <div v-if="urlTrailer" class="modal-trailer">
            <iframe
              :src="urlTrailer"
              frameborder="0"
              allowfullscreen
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
            ></iframe>
          </div>

          <p class="modal-sinopse">{{ movie.overview }}</p>

          <div class="modal-acoes">
            <button class="btn-acao" @click="toggleFavorito">
              {{ ehFavorito ? '❤️ Favoritado' : '🤍 Favoritar' }}
            </button>

            <button v-if="isAdmin" class="btn-acao" @click="modalAberto = false; $emit('editar', movie)">
              ✏️ Editar
            </button>

            <button v-if="isAdmin" class="btn-acao btn-deletar" @click="modalAberto = false; $emit('deletar', movie)">
              🗑️ Deletar
            </button>
          </div>
        </div>
      </div>
    </div>
  </Teleport>
</template>

<style scoped>
.card {
  border-radius: 10px;
  overflow: hidden;
  background: #1a1a1a;
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.2s;
}

.card:hover {
  transform: translateY(-6px);
  box-shadow: 0 12px 32px rgba(229, 9, 20, 0.35);
}

.card-poster {
  position: relative;
  overflow: hidden;
}

.card-poster img {
  width: 100%;
  aspect-ratio: 2/3;
  object-fit: cover;
  display: block;
  transition: transform 0.3s;
}

.card:hover .card-poster img {
  transform: scale(1.05);
}

.btn-fav {
  position: absolute;
  top: 8px;
  left: 8px;
  background: rgba(0, 0, 0, 0.6);
  border: none;
  border-radius: 50%;
  width: 32px;
  height: 32px;
  font-size: 1rem;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: transform 0.15s;
}

.btn-fav:hover {
  transform: scale(1.2);
}

.btn-editar {
  position: absolute;
  top: 8px;
  right: 8px;
  background: rgba(0, 0, 0, 0.7);
  border: 1px solid #e50914;
  color: #fff;
  font-size: 0.75rem;
  padding: 4px 10px;
  border-radius: 6px;
  cursor: pointer;
  opacity: 0;
  transition: opacity 0.2s, background 0.2s;
}

.card:hover .btn-editar {
  opacity: 1;
}

.btn-editar:hover {
  background: #e50914;
}

.card-info {
  padding: 10px 12px;
}

.card-info h3 {
  font-size: 0.95rem;
  font-weight: 700;
  color: #fff;
  margin: 0 0 6px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.card-meta {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.genero {
  font-size: 0.75rem;
  color: #aaa;
  background: #2a2a2a;
  padding: 2px 8px;
  border-radius: 10px;
}

.nota {
  font-size: 0.8rem;
  color: #f5c518;
  font-weight: 600;
}

.modal-fundo {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.85);
  z-index: 1000;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 24px;
}

.modal {
  background: #1a1a1a;
  border-radius: 16px;
  overflow: hidden;
  display: flex;
  max-width: 780px;
  width: 100%;
  max-height: 90vh;
  box-shadow: 0 24px 80px rgba(0, 0, 0, 0.8);
  animation: entrar 0.25s ease;
}

@keyframes entrar {
  from { opacity: 0; transform: scale(0.95); }
  to   { opacity: 1; transform: scale(1); }
}

.modal-poster {
  width: 240px;
  flex-shrink: 0;
  object-fit: cover;
  display: block;
}

.modal-conteudo {
  flex: 1;
  padding: 28px 24px;
  display: flex;
  flex-direction: column;
  gap: 14px;
  position: relative;
  overflow-y: auto;
}

.modal-conteudo h2 {
  color: #fff;
  font-size: 1.5rem;
  font-weight: 700;
  margin: 0;
  padding-right: 36px;
}

.btn-fechar {
  position: absolute;
  top: 14px;
  right: 14px;
  background: rgba(255, 255, 255, 0.08);
  border: none;
  color: #aaa;
  font-size: 1rem;
  width: 30px;
  height: 30px;
  border-radius: 50%;
  cursor: pointer;
  transition: background 0.2s, color 0.2s;
}

.btn-fechar:hover {
  background: #e50914;
  color: #fff;
}

.modal-meta {
  display: flex;
  align-items: center;
  gap: 10px;
  flex-wrap: wrap;
}

.modal-sinopse {
  color: #bbb;
  font-size: 0.9rem;
  line-height: 1.7;
  margin: 0;
}

.modal-trailer {
  width: 100%;
  aspect-ratio: 16/9;
  border-radius: 8px;
  overflow: hidden;
  background: #000;
}

.modal-trailer iframe {
  width: 100%;
  height: 100%;
  border: none;
}

.modal-acoes {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
}

.btn-acao {
  background: #2a2a2a;
  color: #ccc;
  border: 1px solid #444;
  border-radius: 8px;
  font-size: 0.9rem;
  font-weight: 600;
  padding: 10px 20px;
  cursor: pointer;
  transition: background 0.2s;
}

.btn-acao:hover {
  background: #333;
  color: #fff;
}

.btn-deletar {
  color: #e50914;
  border-color: #e50914;
}

.btn-deletar:hover {
  background: #e50914;
  color: #fff;
}
</style>
