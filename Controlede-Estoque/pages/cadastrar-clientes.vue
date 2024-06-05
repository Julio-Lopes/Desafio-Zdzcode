<template>
  <v-sheet class="mx-auto" width="300">
    <v-form fast-fail @submit.prevent="submit">
      <v-text-field
        v-model="nome"
        :rules="nomeRules"
        label="Nome do Cliente"
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
  }),

  methods: {
    async submit() {
      try {
        const response = await this.$axios({
          method: 'POST',
          url: 'https://localhost:7028/Cliente',
          data: {
            nome: this.nome
          }
        });

        if (response.status === 200) {
          this.$router.push({ name: 'clientes' });
        }
      } catch (error) {
        console.error('Erro ao enviar os dados:', error);
      }
    }
  }
}
</script>

