<template>
  <v-data-table
    :headers="headers"
    :items="items"
    item-value="id"
  >
    <template v-slot:item="{ item }">
      <tr>
        <td>{{ item.id }}</td>
        <td>{{ item.nome }}</td>
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
          text: 'Id',
          align: 'start',
          sortable: false,
          value: 'Id',
        },
        { text: 'Nome', value: 'Nome', sortable: false },
        { text: 'Ações', value: 'acoes', sortable: false }
      ],
      items: [], 
    };
  },
  methods: {
    async submit() {
      try {
        const response = await this.$axios.get('https://localhost:7028/Cliente');
        this.items = response.data; 
      } catch (error) {
        console.error(error);
      }
    },
    editarProduto(item) {
      this.$router.push({ name: 'editar-cliente', params: { id: item.id, cliente: item } });
    },
    async deleteProduto(item) {
      try {
        const response = await this.$axios.delete(`https://localhost:7028/Cliente/${item.id}`);
        if (response.status === 204) {
          this.items = this.items.filter(p => p.id !== item.id);
        }
      } catch (error) {
        console.error('Erro ao excluir produto:', error);
      }
    }
  },
  mounted() {
    this.submit();
  },
};
</script>