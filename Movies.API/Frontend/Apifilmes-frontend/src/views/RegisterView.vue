<script lang="ts">
import FormInput from '../components/FormInput.vue'

export default {
  name: 'RegisterView',
  components: { FormInput },
  data() {
    return {
      name: '',
      email: '',
      password: '',
      confirm: '',
    }
  },
}
</script>

<template>
  <div class="auth-page">
    <div class="auth-card">
      <h2 class="auth-card__title">Criar Conta</h2>
      <form class="auth-card__form" @submit.prevent>
        <FormInput v-model="name" label="Nome" placeholder="Seu nome" />
        <FormInput v-model="email" label="E-mail" type="email" placeholder="seu@email.com" />
        <FormInput v-model="password" label="Senha" type="password" placeholder="••••••••" />
        <FormInput v-model="confirm" label="Confirmar Senha" type="password" placeholder="••••••••" />
        <button type="submit" class="auth-card__btn">Cadastrar</button>
      </form>
      <p class="auth-card__footer">
        Já tem conta? <router-link to="/login">Entrar</router-link>
      </p>
    </div>
  </div>
</template>

<style scoped>
.auth-page {
  min-height: calc(100vh - 60px);
  display: flex;
  align-items: center;
  justify-content: center;
  background: #141414;
}

.auth-card {
  background: #1a1a1a;
  padding: 40px;
  border-radius: 12px;
  width: 100%;
  max-width: 400px;
  display: flex;
  flex-direction: column;
  gap: 24px;
}

.auth-card__title {
  margin: 0;
  font-size: 1.75rem;
  color: #fff;
  text-align: center;
}

.auth-card__form {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.auth-card__btn {
  margin-top: 8px;
  padding: 12px;
  background: #e50914;
  color: #fff;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s;
}

.auth-card__btn:hover {
  background: #c40812;
}

.auth-card__footer {
  text-align: center;
  color: #aaa;
  font-size: 0.875rem;
  margin: 0;
}

.auth-card__footer a {
  color: #e50914;
  text-decoration: none;
}
</style>
