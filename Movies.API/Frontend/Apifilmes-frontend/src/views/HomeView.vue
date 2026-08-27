<script lang="ts">
export default {
  name: 'HomeView',
}
</script>
<template>
  <div class="home">
    <div class="home__hero">
      <h1 class="home__title">Bem-vindo ao <span>CineVerse</span></h1>
      <p class="home__subtitle">Descubra, explore e acompanhe seus filmes favoritos.</p>
      <div class="home__actions">
        <router-link to="/filmes" class="home__btn home__btn--primary">Ver Filmes</router-link>
        <router-link to="/cadastro" class="home__btn home__btn--secondary">Criar Conta</router-link>
      </div>
    </div>
    <div class="home__features">
      <div class="home__feature-card">
        <span class="home__feature-icon">🎬</span>
        <h3 class="home__feature-title">Catálogo Completo</h3>
        <p class="home__feature-text">Acesse todos os filmes cadastrados em um só lugar.</p>
      </div>
      <div class="home__feature-card">
        <span class="home__feature-icon">🔐</span>
        <h3 class="home__feature-title">Conta Segura</h3>
        <p class="home__feature-text">Crie sua conta e gerencie seus filmes com segurança.</p>
      </div>
      <div class="home__feature-card">
        <span class="home__feature-icon">⚡</span>
        <h3 class="home__feature-title">Rápido e Simples</h3>
        <p class="home__feature-text">Interface leve e fácil de usar para qualquer pessoa.</p>
      </div>
    </div>
  </div>
</template>
<style scoped>
.home {
  min-height: calc(100vh - 60px);
  background: #141414;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 64px;
  padding: 60px 32px;
}
.home__hero {
  text-align: center;
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
.home__features {
  display: flex;
  gap: 24px;
  flex-wrap: wrap;
  justify-content: center;
  width: 100%;
  max-width: 900px;
}
.home__feature-card {
  background: #1a1a1a;
  border: 1px solid #2a2a2a;
  border-radius: 12px;
  padding: 32px 24px;
  flex: 1;
  min-width: 220px;
  max-width: 280px;
  text-align: center;
  transition: border-color 0.2s, transform 0.2s;
}
.home__feature-card:hover {
  border-color: #e50914;
  transform: translateY(-4px);
}
.home__feature-icon {
  font-size: 2.5rem;
  display: block;
  margin-bottom: 16px;
}
.home__feature-title {
  font-size: 1rem;
  font-weight: 700;
  color: #fff;
  margin-bottom: 8px;
}
.home__feature-text {
  font-size: 0.875rem;
  color: #aaa;
  line-height: 1.6;
}
</style>