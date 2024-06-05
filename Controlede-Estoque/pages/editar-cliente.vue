<template>
  <v-sheet class="mx-auto" width="600">
    <v-form @submit.prevent="submit">
      <v-text-field
        v-model="produto.nome"
        label="Nome do Produto"
        disabled
      ></v-text-field>

      <v-text-field
        v-model="produto.quantidade"
        label="Quantidade"
        type="number"
      ></v-text-field>

      <v-btn class="mt-2" type="submit" block>Atualizar</v-btn>
    </v-form>
  </v-sheet>
</template>

<script>
export default {
  data() {
    return {
      produto: this.$route.params.produto 
    };
  },
  methods: {
    async submit() {
      try {
        const response = await this.$axios.put(`https://localhost:7028/Produto`, {
          id: this.produto.id,
          nome: this.produto.nome,
          quantidade: this.produto.quantidade
        });

        if (response.status === 200) {
          this.$router.push({ name: 'produtos' });
        }
      } catch (error) {
        console.error('Erro ao atualizar produto:', error);
      }
    }
  }
};
</script>


