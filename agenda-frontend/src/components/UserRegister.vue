<template>
  <div class="flex justify-content-center flex-column app-font">
    <!-- <h1 class="green app-font">ContacTrak - Agenda</h1> -->
    <h1 class="green app-font">Registrar novo usuário</h1>
  </div>
  <div class="flex flex-column row-gap-5 app-font">
    <InputGroup>
      <InputGroupAddon>
        <i class="pi pi-user" style="color: green"></i>
      </InputGroupAddon>
      <FloatLabel>
        <InputText id="name" v-model="form.name" />
        <label for="name">Nome</label>
      </FloatLabel>
    </InputGroup>

    <InputGroup>
      <InputGroupAddon>
        <i class="pi pi-envelope" style="color: green"></i>
      </InputGroupAddon>
      <FloatLabel>
        <InputText type="email" id="email" v-model="form.email" />
        <label for="email">E-mail</label>
      </FloatLabel>
    </InputGroup>
    <InputGroup>
      <InputGroupAddon>
        <i class="pi pi-phone" style="color: green"></i>
      </InputGroupAddon>
      <FloatLabel>
        <InputText id="phone" v-model="form.phoneNumber" />
        <label for="phone">Telefone</label>
      </FloatLabel>
    </InputGroup>
    <InputGroup>
      <InputGroupAddon>
        <i class="pi pi-lock" style="color: green"></i>
      </InputGroupAddon>
      <FloatLabel>
        <InputText id="password" type="password" v-model="form.password" />
        <label for="password">Senha</label>
      </FloatLabel>
    </InputGroup>
    <Button
      label="Registrar"
      :disabled="isRegisterDisabled"
      @click="handleRegister"
    />
    <Button label="Voltar" @click="goToLogin" />
  </div>
</template>
  
  <script setup lang="js">
    import InputGroup from 'primevue/inputgroup';
    import InputGroupAddon from 'primevue/inputgroupaddon';
    import InputText from 'primevue/inputtext';
    import FloatLabel from 'primevue/floatlabel';
    import Button from 'primevue/button';
    import { reactive, computed } from 'vue';
    import axios from 'axios';
    import { useRouter } from 'vue-router'; // Importa o roteador

    const router = useRouter(); // Inicializa o roteador
  
    const form = reactive({
      name: '',
      email: '',
      phoneNumber: '',
      password: ''
    });
  
    const isRegisterDisabled = computed(() => {
      return !form.name || !form.email || !form.phoneNumber || !form.password
    });

    const goToLogin = () => {
        router.push('/'); // Navega para a rota '/dashboard'
    };

    const handleRegister = async () => {
    try {
        const response = await axios.post('https://localhost:7233/api/User/Register', {
            name: form.name,
            email: form.email,
            phoneNumber: form.phoneNumber,
            password: form.password,
        });

        console.log('Usuário registrado com sucesso:', response.data);

    } catch (error) {
        console.error('Erro ao registrar usuário:', error);
    } 
};
  </script>
  