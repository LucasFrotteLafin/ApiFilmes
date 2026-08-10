<script lang="ts">
import api from '../api/axios'
export default {
  name: 'LoginView',
  data() {
    return {
      username: '',
      password: '',
      erro: '',
      carregando: false,
    }
  },
  methods: {
    async entrar() {
      this.erro = ''
      this.carregando = true
      try {
        const resposta = await api.post('/login', {
          username: this.username,
          password: this.password,
        })
        const token: string = resposta.data.token
        this.$store.commit('setToken', token)
        this.$router.push('/filmes')
      } catch {
        this.erro = 'Usuário ou senha incorretos.'
      } finally {
        this.carregando = false
      }
    },
  },
}
</script>
<template>
  <div class="auth-page">
    <v-card class="auth-card" color="surface" rounded="lg">
      <v-card-title class="auth-card__title">Entrar</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="entrar">
          <v-text-field
            v-model="username"
            placeholder="Seu usuário"
            variant="underlined"
            color="primary"
            class="mb-3"
          />
          <v-text-field
            v-model="password"
            type="password"
            placeholder="••••••••"
            variant="underlined"
            color="primary"
            class="mb-2"
          />
          <p v-if="erro" class="auth-erro">{{ erro }}</p>
          <v-btn
            type="submit"
            color="primary"
            block
            size="large"
            :loading="carregando"
            class="mt-2"
          >
            Entrar
          </v-btn>
        </v-form>
      </v-card-text>
      <v-card-text class="text-center auth-footer">
        Não tem conta?
        <router-link to="/cadastro" class="auth-link">Cadastre-se</router-link>
      </v-card-text>
    </v-card>
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
  width: 100%;
  max-width: 400px;
  padding: 16px 32px;
}
.auth-card :deep(.v-card-text) {
  padding-left: 32px;
  padding-right: 32px;
}
.auth-card :deep(.v-field__input) {
  padding-left: 16px;
}
.auth-card :deep(.v-label) {
  margin-left: 16px;
}
.auth-card__title {
  font-size: 1.75rem;
  font-weight: 700;
  text-align: center;
  padding-top: 16px;
}
.auth-erro {
  color: #e50914;
  font-size: 0.875rem;
  text-align: center;
  margin-bottom: 8px;
}
.auth-footer {
  color: #aaa;
  font-size: 0.875rem;
}
.auth-link {
  color: #e50914;
  text-decoration: none;
}
</style>