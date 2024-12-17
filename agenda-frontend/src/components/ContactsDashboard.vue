<template>
  <div class="flex flex-column row-gap-5 app-font">
    <DataTable :value="form.contacts" tableStyle="min-width: 50rem">
      <Column field="id" header="ID"></Column>
      <Column field="name" header="Nome"></Column>
      <Column field="email" header="E-mail"></Column>
      <Column field="phoneNumber" header="Telefone"></Column>
      <Column class="w-24 !text-end">
        <template #body="{ data }">
          <div>
            <!-- Botão de Editar -->
            <Button label="Editar" @click="openEditDialog(data)" class="p-button-sm" />
            <Button label="Delete" @click="confirmDeleteContact(data)" class="p-button-sm p-button-danger" severity="danger" />
          </div>
        </template>
      </Column>
    </DataTable>

    <div app-font>
      <Dialog v-model:visible="deleteDialogVisible" header="Confirmação" :closable="false" modal>
      <p>Tem certeza que deseja deletar o contato <b>{{ contactToDelete?.name }}</b>?</p>
      <template #footer>
        <Button label="Cancelar" icon="pi pi-times" @click="deleteDialogVisible = false" class="p-button-text" />
        <Button 
          label="Deletar" 
          icon="pi pi-check" 
          @click="deleteContact" 
          severity="danger" 
        />
      </template>
    </Dialog>
    </div>
  </div>
</template>

<script setup lang="js">
  import { reactive, onMounted, ref } from 'vue';
  import axios from 'axios';
  import DataTable from 'primevue/datatable';
  import Column from 'primevue/column';
  import Button from 'primevue/button';
  import Dialog from 'primevue/dialog';
  // import InputText from 'primevue/inputtext';

  // const router = useRouter(); 

  const form = reactive({
      contacts: []
    });

  // Variáveis de controle do dialog de deletar contato
  const deleteDialogVisible = ref(false);
  const contactToDelete = ref(null);

  // const getTokenConfig = () => {
  //   const token = localStorage.getItem('token');
  //   return {
  //     headers: {
  //       Authorization: `Bearer ${token}`,
  //     }
  //   };
  // };

  const token = localStorage.getItem('token');

  // console.log("THIS IS MY TOKEN", getTokenConfig().headers);

//Listar contatos
const listContacts = async () => {
  try {
    const response = await axios.get(`https://localhost:7233/api/Agenda/ListInformations`, {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    });
    console.log(response.data);
    
    form.contacts = response.data.data.map((contact) => ({
      id: contact.id, 
      name: contact.name, 
      email: contact.email, 
      phoneNumber: contact.phoneNumber,
    }));

    console.log('Contatos carregados:', form.contacts);
  } catch (error) {
    console.error('Erro ao listar contatos:', error);
  }
};

//Abrir a confirmação do delete
const confirmDeleteContact = (contact) => {
  contactToDelete.value = contact;
  deleteDialogVisible.value = true;
}

//Deletar o contato
const deleteContact = async () => {
  if (!contactToDelete.value) {
    return;
} 

  try {
    const contactId = contactToDelete.value.id;
    console.log("THIS IS MY ID: ", contactId);
    const response = await axios.delete(`https://localhost:7233/api/Agenda/DeleteInformation/${contactId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    });
    
    // Remove o contato da lista do front
    form.contacts = form.contacts.filter(contact => contact.id !== contactId);

    console.log(response);
  } catch (error) {
    console.error('Erro ao deletar contato:', error);
  } finally {
    // Fecha o diálogo
    deleteDialogVisible.value = false;
    contactToDelete.value = null;
  }
};

// Chama a função que monta o componente
onMounted(() => {
  listContacts();
});

</script>
