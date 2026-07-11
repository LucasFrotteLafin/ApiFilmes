j<script lang="ts">
export default {
  props: {
    label: { type: String, required: true },
    type: { type: String, default: 'text' },
    modelValue: { type: String, default: '' },
    placeholder: { type: String, default: '' },
  },
  emits: ['update:modelValue'],
}
</script>

<template>
  <div class="form-input">
    <label class="form-input__label">{{ label }}</label>
    <input
      :type="type"
      :value="modelValue"
      :placeholder="placeholder"
      class="form-input__field"
      @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
    />
  </div>
</template>

<style scoped>
.form-input {
  display: flex;
  flex-direction: column;
  gap: 6px;
  width: 100%;
}

.form-input__label {
  font-size: 0.875rem;
  font-weight: 600;
  color: #ccc;
}

.form-input__field {
  padding: 10px 14px;
  border: 1px solid #444;
  border-radius: 8px;
  background: #2a2a2a;
  color: #fff;
  font-size: 1rem;
  outline: none;
  transition: border-color 0.2s;
}

.form-input__field:focus {
  border-color: #e50914;
}
</style>
