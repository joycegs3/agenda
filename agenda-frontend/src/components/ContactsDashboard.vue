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
            <!-- Edit button -->
            <Button
              label="Editar"
              @click="openEditDialog(data)"
              class="p-button-sm"
              severity="primary"
            />
            <!-- Delete Button -->
            <Button
              label="Delete"
              @click="confirmDeleteContact(data)"
              class="p-button-sm p-button-danger"
              severity="danger"
            />
          </div>
        </template>
      </Column>
    </DataTable>
    <!-- Add Button  -->
    <Button
      label="Adicionar Contato"
      icon="pi pi-plus"
      class="p-button-success mt-3"
      @click="openNewContactDialog"
    />

    <!-- Delete dialog -->
    <Dialog
      v-model:visible="deleteDialogVisible"
      header="Confirmação"
      :closable="false"
      modal
    >
      <p>
        Tem certeza que deseja deletar o contato
        <b>{{ contactToDelete?.name }}</b
        >?
      </p>
      <template #footer>
        <Button
          label="Cancelar"
          @click="deleteDialogVisible = false"
          class="p-button-text"
        />
        <Button label="Deletar" @click="deleteContact" severity="danger" />
      </template>
    </Dialog>

    <!-- Edit dialog -->
    <Dialog
      v-model:visible="editDialogVisible"
      header="Contato"
      :closable="true"
      modal
    >
      <div class="p-grid p-fluid">
        <div class="p-field p-col-12 p-md-6">
          <label for="name">Nome</label>
          <InputText v-model="currentContact.name" id="name" />
        </div>
        <div class="p-field p-col-12 p-md-6">
          <label for="email">E-mail</label>
          <InputText v-model="currentContact.email" id="email" />
        </div>
        <div class="p-field p-col-12 p-md-6">
          <label for="phoneNumber">Telefone</label>
          <InputText v-model="currentContact.phoneNumber" id="phoneNumber" />
        </div>
      </div>
      <template #footer>
        <Button
          label="Cancelar"
          @click="closeEditDialog"
          class="p-button-text"
        />
        <Button label="Salvar" @click="saveContact" class="p-button-primary" />
      </template>
    </Dialog>
  </div>
</template>

<script setup lang="js">
  import { reactive, onMounted, ref } from 'vue';
  import axios from 'axios';
  import DataTable from 'primevue/datatable';
  import Column from 'primevue/column';
  import Button from 'primevue/button';
  import Dialog from 'primevue/dialog';
  import InputText from 'primevue/inputtext';

  // const router = useRouter(); 

  const form = reactive({
      contacts: []
    });

  // Dialog control variables
  const deleteDialogVisible = ref(false);
  const contactToDelete = ref(null);
  const editDialogVisible = ref(false);
  const currentContact = reactive({
    id: null,
    name: '',
    email: '',
    phoneNumber: ''
  });

  const token = localStorage.getItem('token');

//List contacts
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

//Open delete contact confirmation dialog
const confirmDeleteContact = (contact) => {
  contactToDelete.value = contact;
  deleteDialogVisible.value = true;
}

//Delete contact
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
    
    // Remove contact from list on the frontend
    form.contacts = form.contacts.filter(contact => contact.id !== contactId);

    console.log(response);
  } catch (error) {
    console.error('Erro ao deletar contato:', error);
  } finally {
    // Close dialog
    deleteDialogVisible.value = false;
    contactToDelete.value = null;
  }
};

// Open add new contact dialog
const openNewContactDialog = () => {
  currentContact.id = null;
  currentContact.name = '';
  currentContact.email = '';
  currentContact.phoneNumber = '';
  editDialogVisible.value = true;
};

// Open edit contact dialog
const openEditDialog = (contact) => {
  currentContact.id = contact.id;
  currentContact.name = contact.name;
  currentContact.email = contact.email;
  currentContact.phoneNumber = contact.phoneNumber;
  editDialogVisible.value = true;
};

// Close the add/edit dialog
const closeEditDialog = () => {
  editDialogVisible.value = false;
};

// Save or edit contact
const saveContact = async () => {
  try {
    if (currentContact.id) {
      // Edit existing contact
      await axios.put(`https://localhost:7233/api/Agenda/EditInformation`, {
        id: currentContact.id,
        name: currentContact.name,
        email: currentContact.email,
        phoneNumber: currentContact.phoneNumber
      }, {
        headers: {
          Authorization: `Bearer ${token}`,
        }
      });
      // Refresh the infos list
      const index = form.contacts.findIndex(contact => contact.id === currentContact.id);
      if (index !== -1) {
        form.contacts[index] = { ...currentContact };
      }
    } else {
      // Add new contact
      const response = await axios.post(`https://localhost:7233/api/Agenda/CreateInformation`, {
        name: currentContact.name,
        email: currentContact.email,
        phoneNumber: currentContact.phoneNumber
      }, {
        headers: {
          Authorization: `Bearer ${token}`,
        }
      });

      form.contacts.push(response.data);
      listContacts();
    }
  } catch (error) {
    console.error('Erro ao salvar contato:', error);
  } finally {
    closeEditDialog();
  }
};

// Calls the function to mount the component
onMounted(() => {
  listContacts();
});

</script>
