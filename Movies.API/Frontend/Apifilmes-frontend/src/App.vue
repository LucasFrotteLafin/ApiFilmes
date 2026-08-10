<script lang="ts">
import Navbar from './components/Navbar.vue'
export default {
  components: { Navbar },
  computed: {
    temaClaro() {
      return !this.$store.getters.darkMode
    },
  },
  watch: {
    temaClaro: {
      immediate: true,
      handler(valor: boolean) {
        if (valor) {
          document.body.classList.add('tema-claro')
        } else {
          document.body.classList.remove('tema-claro')
        }
      }
    },
  },
  mounted() {
    if (this.temaClaro) {
      document.body.classList.add('tema-claro')
    } else {
      document.body.classList.remove('tema-claro')
    }
  },
}
</script>
<template>
  <v-app>
    <Navbar />
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>