<script lang="ts">
import api from '../api/axios'

export default {
  name: 'RegisterView',
  data() {
    return {
      username: '',
      password: '',
      confirm: '',
      erro: '',
      sucesso: '',
      carregando: false,
    }
  },
  methods: {
    async cadastrar() {
      this.erro = ''
      this.sucesso = ''

      if (this.password !== this.confirm) {
        this.erro = 'As senhas não coincidem.'
        return
      }

      this.carregando = true
      try {
        await api.post('/user', {
          username: this.username,
          password: this.password,
        })
        this.sucesso = 'Conta criada! Redirecionando...'
        setTimeout(() => this.$router.push('/login'), 1500)
      } catch {
        this.erro = 'Erro ao criar conta. Tente novamente.'
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
      <v-card-title class="auth-card__title">Criar Conta</v-card-title>
      <v-card-text class="auth-card__body">
        <v-form @submit.prevent="cadastrar">
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
            class="mb-3"
          />
          <v-text-field
            v-model="confirm"
            type="password"
            placeholder="••••••••"
            variant="underlined"
            color="primary"
            class="mb-1"
          />
          <p class="auth-regras">As senhas devem ser iguais.</p>
          <p v-if="erro" class="auth-erro">{{ erro }}</p>
          <p v-if="sucesso" class="auth-sucesso">{{ sucesso }}</p>
          <v-btn
            type="submit"
            color="primary"
            block
            size="large"
            :loading="carregando"
            class="mt-3"
          >
            Cadastrar
          </v-btn>
        </v-form>
      </v-card-text>
      <v-card-text class="text-center auth-footer">
        Já tem conta?
        <router-link to="/login" class="auth-link">Entrar</router-link>
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

.auth-regras {
  color: #888;
  font-size: 0.8rem;
  margin-bottom: 8px;
}

.auth-erro {
  color: #e50914;
  font-size: 0.875rem;
  text-align: center;
  margin-bottom: 8px;
}

.auth-sucesso {
  color: #4caf50;
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
