import { createStore } from 'vuex'

interface State {
  token: string
  favorites: number[]
}

function parseRole(token: string): string {
  try {
    const payload = JSON.parse(atob(token.split('.')[1]))
    return payload['role'] || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
  } catch {
    return ''
  }
}

export default createStore<State>({
  state: {
    token: localStorage.getItem('token') || '',
    favorites: JSON.parse(localStorage.getItem('favorites') || '[]'),
  },
  getters: {
    logado: (state) => !!state.token,
    isAdmin: (state) => parseRole(state.token) === 'Admin',
    favorites: (state) => state.favorites,
    isFavorite: (state) => (id: number) => state.favorites.includes(id),
  },
  mutations: {
    setToken(state, token: string) {
      state.token = token
      localStorage.setItem('token', token)
    },
    logout(state) {
      state.token = ''
      state.favorites = []
      localStorage.removeItem('token')
      localStorage.removeItem('favorites')
    },
    setFavorites(state, ids: number[]) {
      state.favorites = ids
      localStorage.setItem('favorites', JSON.stringify(ids))
    },
    addFavorite(state, id: number) {
      state.favorites = [...state.favorites, id]
      localStorage.setItem('favorites', JSON.stringify(state.favorites))
    },
    removeFavorite(state, id: number) {
      state.favorites = state.favorites.filter(f => f !== id)
      localStorage.setItem('favorites', JSON.stringify(state.favorites))
    },
  },
})