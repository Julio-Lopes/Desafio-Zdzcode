<template>
  <v-sheet class="mx-auto" width="300">
    <v-form fast-fail @submit.prevent="submit">
      <v-text-field
        v-model="nome"
        :rules="nomeRules"
        label="Nome do Produto"
      ></v-text-field>

      <v-text-field
        v-model="quantidade"
        :rules="quantidadeRules"
        label="Quantidade"
      ></v-text-field>

      <v-btn class="mt-2" type="submit" block>Submit</v-btn>
    </v-form>
  </v-sheet>
</template>

<script>
export default {
  data: () => ({
    nome: '',
    nomeRules: [],
    quantidade: '',
    quantidadeRules: [
      value => Number.isInteger(parseInt(value)) && !isNaN(parseInt(value)) || 'A quantidade deve ser um número inteiro'
    ],
  }),

  methods: {
    async submit() {
      try {
        const response = await this.$axios({
          method: 'POST',
          url: 'https://localhost:7028/Produto',
          data: {
            nome: this.nome,
            quantidade: this.quantidade
          }
        });

        if (response.status === 200) {
          this.$router.push({ name: 'produtos' });
        }
      } catch (error) {
        console.error('Erro ao enviar os dados:', error);
      }
    }
  }
}
</script>

