<script lang="ts">
export default {
  name: 'HomeView',
}
</script>

<template>
  <div class="home">
    <div class="home__hero">
      <h1 class="home__title">Bem-vindo ao <span>ApiFilmes</span></h1>
      <p class="home__subtitle">Descubra, explore e acompanhe seus filmes favoritos.</p>
      <div class="home__actions">
        <router-link to="/filmes" class="home__btn home__btn--primary">Ver Filmes</router-link>
        <router-link to="/cadastro" class="home__btn home__btn--secondary">Criar Conta</router-link>
      </div>
    </div>
  </div>
</template>

<style scoped>
.home {
  min-height: calc(100vh - 60px);
  background: #141414;
  display: flex;
  align-items: center;
  justify-content: center;
}

.home__hero {
  text-align: center;
  padding: 40px 20px;
}

.home__title {
  font-size: 3rem;
  color: #fff;
  margin: 0 0 16px;
}

.home__title span {
  color: #e50914;
}

.home__subtitle {
  font-size: 1.2rem;
  color: #aaa;
  margin: 0 0 40px;
}

.home__actions {
  display: flex;
  gap: 16px;
  justify-content: center;
  flex-wrap: wrap;
}

.home__btn {
  padding: 14px 32px;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  text-decoration: none;
  transition: opacity 0.2s;
}

.home__btn:hover {
  opacity: 0.85;
}

.home__btn--primary {
  background: #e50914;
  color: #fff;
}

.home__btn--secondary {
  background: transparent;
  color: #fff;
  border: 2px solid #fff;
}
</style>
