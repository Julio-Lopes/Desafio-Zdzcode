<template>
  <v-sheet class="mx-auto" width="500">
    <v-form @submit.prevent="submit">
      <v-select
        v-model="idCliente"
        :items="clientes"
        item-value="id"
        item-text="nome"
        label="Selecione o Cliente"
      ></v-select>

      <v-select
        v-model="idProduto"
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
    idCliente: '',
    idProduto: '',
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
        const response = await this.$axios.get('https://localhost:7028/Cliente');
        this.clientes = response.data;
      } catch (error) {
        console.error('Erro ao buscar clientes:', error);
      }
    },

    async fetchProdutos() {
      try {
        const response = await this.$axios.get('https://localhost:7028/Produto');
        this.produtos = response.data;
      } catch (error) {
        console.error('Erro ao buscar produtos:', error);
      }
    },

    async submit() {
      try {
        const cliente = this.clientes.find(c => c.id === this.idCliente);
        const produto = this.produtos.find(p => p.id === this.idProduto);

            
        if (parseInt(this.quantidade) > produto.quantidade) {
        alert('Quantidade excede a disponível para o produto.');
        return; 
        }

        const response = await this.$axios({
          method: 'POST',
          url: 'https://localhost:7028/SaidaProduto',
          data: {
            idCliente: cliente.id,
            idProduto: produto.id,
            quantidade: this.quantidade
          }
        });

        if (response.status === 200) {
          this.$router.push({ name: 'lista-saida-produtos' });
        }
      } catch (error) {
        console.error('Erro ao enviar os dados:', error);
      }
    }
  }
}
</script>

