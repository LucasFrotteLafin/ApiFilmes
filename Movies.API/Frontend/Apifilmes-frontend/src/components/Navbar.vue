<script lang="ts">
export default {
  name: 'Navbar',
  data() {
    return { scrolled: false }
  },
  computed: {
    logado() {
      return this.$store.getters.logado
    },
    isAdmin() {
      return this.$store.getters.isAdmin
    },
    temaClaro() {
      return !this.$store.getters.darkMode
    },
  },
  methods: {
    onScroll() {
      this.scrolled = window.scrollY > 10
    },
    sair() {
      this.$store.commit('logout')
      this.$router.push('/login')
    },
    alternarTema() {
      this.$store.commit('toggleDarkMode')
    },
  },
  mounted() {
    window.addEventListener('scroll', this.onScroll)
  },
  beforeUnmount() {
    window.removeEventListener('scroll', this.onScroll)
  },
}
</script>
<template>
  <nav class="navbar" :class="{ 'navbar--scrolled': scrolled }">
    <router-link to="/" class="navbar__brand">CineVerse</router-link>
    <ul class="navbar__list">
      <li><router-link to="/" class="navbar__link">Início</router-link></li>
      <li><router-link to="/filmes" class="navbar__link">Filmes</router-link></li>
      <template v-if="logado">
        <li><router-link to="/favoritos" class="navbar__link">Favoritos</router-link></li>
        <li v-if="isAdmin"><router-link to="/admin" class="navbar__link navbar__link--admin">Admin</router-link></li>
        <li><button class="navbar__link navbar__link--sair" @click="sair">Sair</button></li>
      </template>
      <template v-else>
        <li><router-link to="/login" class="navbar__link">Login</router-link></li>
        <li><router-link to="/cadastro" class="navbar__link navbar__link--cta">Cadastrar</router-link></li>
      </template>
      <li>
        <button class="navbar__link navbar__link--tema" @click="alternarTema">
          {{ temaClaro ? '🌙 Escuro' : '☀️ Claro' }}
        </button>
      </li>
    </ul>
  </nav>
</template>
<style scoped>
.navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 32px;
  height: 60px;
  background: #111;
  border-bottom: 2px solid #e50914;
  position: sticky;
  top: 0;
  z-index: 100;
  transition: background 0.3s, box-shadow 0.3s;
}
.navbar--scrolled {
  background: rgba(10, 10, 10, 0.97);
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.6);
}
.navbar__brand {
  font-size: 1.25rem;
  font-weight: 700;
  color: #e50914;
  text-decoration: none;
}
.navbar__list {
  display: flex;
  list-style: none;
  margin: 0;
  padding: 0;
  gap: 24px;
  align-items: center;
}
.navbar__link {
  text-decoration: none;
  color: #ccc;
  font-size: 0.95rem;
  transition: color 0.2s;
}
.navbar__link:hover,
.navbar__link.router-link-active {
  color: #fff;
}
.navbar__link--admin {
  color: #e50914;
  font-weight: 600;
  border: 1px solid #e50914;
  padding: 6px 16px;
  border-radius: 6px;
}
.navbar__link--admin:hover {
  background: #e50914;
  color: #fff;
}
.navbar__link--cta {
  background: #e50914;
  color: #fff;
  padding: 6px 16px;
  border-radius: 6px;
}
.navbar__link--cta:hover {
  background: #c40812;
  color: #fff;
}
.navbar__link--sair {
  background: none;
  border: 1px solid #ccc;
  padding: 6px 16px;
  border-radius: 6px;
  cursor: pointer;
}
.navbar__link--tema {
  background: none;
  border: 1px solid #555;
  padding: 6px 14px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 0.85rem;
}
.navbar__link--tema:hover {
  border-color: #fff;
  color: #fff;
}
</style>