<template>
  <v-sheet class="mx-auto" width="500">
    <v-form @submit.prevent="submit">
      <v-select
        v-model="clienteId"
        :items="clientes"
        item-value="id"
        item-text="nome"
        label="Selecione o Cliente"
      ></v-select>

      <v-select
        v-model="produtoId"
        :items="produtos"
        item-value="id"
        item-text="nome"
        label="Selecione o Produto"
      ></v-select>

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
    clienteId: '',
    produtoId: '',
    quantidade: '',
    quantidadeRules: [
      value => Number.isInteger(parseInt(value)) && !isNaN(parseInt(value)) || 'A quantidade deve ser um número inteiro'
    ],
    clientes: [],
    produtos: []
  }),

  async mounted() {
    await this.fetchClientes();
    await this.fetchProdutos();
  },

  methods: {
    async fetchClientes() {
      try {
        const response = await this.$axios.get('https://localhost:7028/Clientes');
        this.clientes = response.data;
      } catch (error) {
        console.error('Erro ao buscar clientes:', error);
      }
    },

    async fetchProdutos() {
      try {
        const response = await this.$axios.get('https://localhost:7028/Produtos');
        this.produtos = response.data;
      } catch (error) {
        console.error('Erro ao buscar produtos:', error);
      }
    },

    async submit() {
      try {
        const response = await this.$axios({
          method: 'POST',
          url: 'https://localhost:7028/Produto',
          data: {
            clienteId: this.clienteId,
            produtoId: this.produtoId,
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
