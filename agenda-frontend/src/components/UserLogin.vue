<template>
  <div class="flex flex-column row-gap-5 app-font">
    <InputGroup>
      <InputGroupAddon>
        <i class="pi pi-envelope" style="color: green"></i>
      </InputGroupAddon>
      <FloatLabel>
        <InputText id="email" v-model="form.email" />
        <label for="email">E-mail</label>
      </FloatLabel>
    </InputGroup>

    <InputGroup>
      <InputGroupAddon>
        <i class="pi pi-lock" style="color: green"></i>
      </InputGroupAddon>
      <FloatLabel>
        <InputText type="password" id="password" v-model="form.password" />
        <label for="password">Senha</label>
      </FloatLabel>
    </InputGroup>
    <Button label="Login" :disabled="isLoginDisabled" @click="handleLogin"/>
    <Button label="Registrar" @click="goToRegister"/>
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
    email: '',
    password: ''
  });

  const isLoginDisabled = computed(() => {
    return !form.email || !form.password;
  });

  const goToRegister = () => {
    router.push('/register'); 
  };


  const handleLogin = async () => {
    const response = await axios.get(`https://localhost:7233/api/User/Login/${form.email}/${form.password}`, {
      email: form.email,
      password: form.password
    })

    if (response.data.id > 0) {
      localStorage.setItem('token', response.data.token);
      response.data.token = "";
      localStorage.setItem('user', response.data);
      router.push('/dashboard');
    }

    // console.log(response);
  }

</script>