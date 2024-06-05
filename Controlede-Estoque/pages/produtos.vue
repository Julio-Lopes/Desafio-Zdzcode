<template>
  <v-data-table
    :headers="headers"
    :items="items"
    item-value="id"
  >
    <template v-slot:item="{ item }">
      <tr>
        <td>{{ item.nome }}</td>
        <td>{{ item.quantidade }}</td>
        <td>
          <v-btn color="blue" @click="editarProduto(item)">Atualizar</v-btn>
          <v-btn color="red" @click="deleteProduto(item)">Remover</v-btn>
        </td>
      </tr>
    </template>
  </v-data-table>
</template>

<script>
export default {
  data() {
    return {
      headers: [
        {
          text: 'Nome',
          align: 'start',
          sortable: false,
          value: 'nome',
        },
        { text: 'Quantidade', value: 'quantidade' },
        { text: 'Ações', value: 'acoes', sortable: false }
      ],
      items: [], // Inicialmente vazio
    };
  },
  methods: {
    async submit() {
      try {
        const response = await this.$axios.get('https://localhost:7028/Produto');
        this.items = response.data; // Atualiza a lista de items com a resposta da API
      } catch (error) {
        console.error(error);
      }
    },
    editarProduto(item) {
      this.$router.push({ name: 'editar-produto', params: { id: item.id, produto: item } });
    },
  async deleteProduto(item) {
    try {
      const data = {
        id: item.id,
        nome: item.nome,
        quantidade: item.quantidade
      };

      const response = await this.$axios.delete(`https://localhost:7028/Produto`, { data });
      console.log('Resposta da API:', response);

      if (response.status === 200) {
        this.items = this.items.filter(p => p.id !== item.id);
        console.log('Item excluído com sucesso');
      } else {
        console.log('Falha ao excluir o item');
      }
    } catch (error) {
      console.error('Erro ao excluir produto:', error);
    }
  }

  },
  mounted() {
    this.submit(); // Chama o método ao montar o componente
  },
};
</script>
