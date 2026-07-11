<script lang="ts">
export default {
  name: 'Navbar',
}
</script>

<template>
  <nav class="navbar">
    <router-link to="/" class="navbar__brand">ApiFilmes</router-link>
    <ul class="navbar__list">
      <li><router-link to="/" class="navbar__link">Início</router-link></li>
      <li><router-link to="/filmes" class="navbar__link">Filmes</router-link></li>
      <li><router-link to="/login" class="navbar__link">Login</router-link></li>
      <li>
        <router-link to="/cadastro" class="navbar__link navbar__link--cta">Cadastrar</router-link>
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
</style>
