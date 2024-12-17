<template>
  <div class="hello">
    <Form
      v-slot="$form"
      :resolver="resolver"
      :initialValues="initialValues"
      @submit="onFormSubmit"
      class="flex justify-center flex-col gap-4"
    >
      <div class="flex flex-col gap-1">
        <InputText name="username" type="text" placeholder="Username" />
        <Message
          v-if="$form.username?.invalid"
          severity="error"
          size="small"
          variant="simple"
          >{{ $form.username.error?.message }}</Message
        >
      </div>
      <div class="flex flex-col gap-1">
        <InputText name="email" type="text" placeholder="Email" />
        <Message
          v-if="$form.email?.invalid"
          severity="error"
          size="small"
          variant="simple"
          >{{ $form.email.error?.message }}</Message
        >
      </div>
      <Button type="submit" severity="secondary" label="Submit" />
    </Form>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
  props: {
    msg: String,
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
