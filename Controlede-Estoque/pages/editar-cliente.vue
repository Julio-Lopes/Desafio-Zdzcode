<template>
  <v-sheet class="mx-auto" width="600">
    <v-form @submit.prevent="submit">
      <v-text-field
        v-model="cliente.nome"
        label="Nome do Cliente"
      ></v-text-field>

      <v-btn class="mt-2" type="submit" block>Atualizar</v-btn>
    </v-form>
  </v-sheet>
</template>

<script>
export default {
  data() {
    return {
      cliente: this.$route.params.cliente 
    };
  },
  methods: {
    async submit() {
      try {
        const response = await this.$axios.put(`https://localhost:7028/Cliente`, {
          id: this.cliente.id,
          nome: this.cliente.nome,
        });

        if (response.status === 200) {
          this.$router.push({ name: 'clientes' });
        }
      } catch (error) {
        console.error('Erro ao atualizar cliente:', error);
      }
    }
  }
};
</script>


