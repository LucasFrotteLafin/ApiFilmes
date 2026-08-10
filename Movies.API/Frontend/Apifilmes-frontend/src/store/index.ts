import { createStore } from 'vuex'

interface State {
  token: string
}

function parseRole(token: string): string {
  try {
    const payload = JSON.parse(atob(token.split('.')[1]))
    return payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || ''
  } catch {
    return ''
  }
}

export default createStore<State>({
  state: {
    token: localStorage.getItem('token') || '',
  },
  getters: {
    logado: (state: State) => !!state.token,
    isAdmin: (state: State) => parseRole(state.token) === 'Admin',
  },
  mutations: {
    setToken(state: State, token: string) {
      state.token = token
      localStorage.setItem('token', token)
    },
    logout(state: State) {
      state.token = ''
      localStorage.removeItem('token')
    },
  },
})
