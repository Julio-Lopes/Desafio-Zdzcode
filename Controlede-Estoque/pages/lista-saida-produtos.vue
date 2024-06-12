<template>
  <v-data-table
    :headers="headers"
    :items="items"
    item-value="id"
  >
    <template v-slot:item="{ item }">
      <tr>
        <td>{{ item.id }}</td>
        <td>{{ item.nomeCliente }}</td>
        <td>{{ item.nomeProduto }}</td>
        <td>{{ item.quantidade }}</td>
        <td>
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
          text: 'Id',
          align: 'start',
          sortable: false,
          value: 'Id',
        },
        { text: 'Cliente', value: 'nomeCliente', sortable: false },
        { text: 'Produto', value: 'nomeProduto', sortable: false },
        { text: 'Quantidade', value: 'quantidade', sortable: false },
        { text: 'Ações', value: 'acoes', sortable: false }
      ],
      items: [], 
    };
  },
  methods: {
    async submit() {
      try {
        const response = await this.$axios.get('https://localhost:7028/SaidaProduto');
        this.items = response.data;
      } catch (error) {
        console.error(error);
      }
    },
    async deleteProduto(item) {
      try {
        const response = await this.$axios.delete(`https://localhost:7028/SaidaProduto/${item.id}`);
        if (response.status === 200) {
          this.items = this.items.filter(p => p.id !== item.id);
        }
      } catch (error) {
        console.error('Erro ao excluir nota de saida:', error);
      }
    }
  },
  mounted() {
    this.submit();
  },
};
</script>
