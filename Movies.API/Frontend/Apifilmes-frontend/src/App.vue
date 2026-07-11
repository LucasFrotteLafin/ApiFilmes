<script lang="ts">
import Navbar from './components/Navbar.vue'

export default {
  components: { Navbar },
}
</script>

<template>
  <div>
    <Navbar />
    <router-view />
  </div>
</template>
