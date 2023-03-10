<style scoped lang="scss">
.sign-up-form{
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
  .sign-up-information{
      width: 35%;
      .sign-up-body{
        overflow: auto;
        height: 80vh;
      }
      .description-scope{
        color: red;
        font-style: italic;
      }

      label::after {
        content: '*';
        color: red;
      }
      .error-message{
        color: red;
        font-size: 13px;
        font-style: italic;
      }
      .checkbox-label::after {
        content: ''!important;
      }
      .checkbox-label {
        width: fit-content;
      }
      .divider{
        border-top: 1px solid #dedede;
      }
      .firstname-information{
          display: flex;
          flex-direction: column;
      }
      .lastname-information{
          display: flex;
          flex-direction: column;
      }
      .email-information{
          display: flex;
          flex-direction: column;
      }
      .action-form{
          div{
              display: flex;
              justify-content: space-between;
              a, button{
                  width: 50%;
              }
          }
      }
  }
}

@media only screen and (max-width: 1000px){
    .sign-up-information{
        width: 100%!important;
    }
}
</style>

<template>
    <div class="container-fluid">
      <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 sign-up-form">
              <div class="card sign-up-information">
                <div class="card-header text-center"><h4>Đăng Kí Tài Khoản</h4></div>
                <div class="card-body sign-up-body">
                  <p class="description-scope">Thông tin người dùng</p>
                  <div class="mb-3 row firstname-information">
                    <label class="col-12 col-form-label">Họ:</label>
                    <div class="col-12">
                        <input type="text" class="form-control" maxlength="100" v-model="model.firstname.value" :disabled="isDisabled">
                        <span class="error-message">{{ model.firstname.error }}</span>
                    </div>
                  </div>
                  <div class="mb-3 row lastname-information">
                    <label class="col-12 col-form-label">Tên:</label>
                    <div class="col-12">
                        <input type="text" class="form-control" maxlength="100" v-model="model.lastname.value" :disabled="isDisabled">
                        <span class="error-message">{{ model.lastname.error }}</span>
                    </div>
                  </div>
                  <div class="mb-3 row email-information">
                    <label class="col-12 col-form-label">Email:</label>
                    <div class="col-12">
                        <input type="email" class="form-control" maxlength="100" v-model="model.email.value" :disabled="isDisabled">
                        <span class="error-message">{{ model.email.error }}</span>
                    </div>
                  </div>
                  <p class="description-scope divider">Thông tin tài khoản</p>
                  <div class="mb-3 row">
                      <label class="col-12 col-form-label checkbox-label">
                      <input type="checkbox" v-model="emailAsUsername"/> Sử dụng email làm tên đăng nhập.</label>
                  </div>
                  <div class="mb-3 row username-information">
                      <label class="col-12 col-form-label">Tên đăng nhập:</label>
                      <div class="col-12">
                          <input type="text" class="form-control" maxlength="100" v-model="model.username.value" :disabled="isDisabled" :readonly="isReadOnly">
                          <span class="error-message">{{ model.username.error }}</span>
                      </div>
                  </div>
                  <div class="mb-3 row pwd-information">
                      <label class="col-12 col-form-label">Mật khẩu:</label>
                      <div class="col-12">
                          <input type="password" class="form-control" maxlength="20" v-model="model.password.value" :disabled="isDisabled">
                          <span class="error-message">{{ model.password.error }}</span>
                      </div>
                  </div>
                  <div class="mb-3 row confirm-pwd-information">
                      <label class="col-12 col-form-label">Nhập lại Mật khẩu:</label>
                      <div class="col-12">
                          <input type="password" class="form-control" maxlength="20" v-model="model.confirmPassword.value" :disabled="isDisabled">
                          <span class="error-message">{{ model.confirmPassword.error }}</span>
                      </div>
                  </div>
                </div>
                <div class="card-footer">
                  <div class="row action-form">
                      <div class="col-12">
                          <router-link to="/login">Quay lại đăng nhập</router-link>
                          <button class="btn btn-primary" @click="registerAccount" :disabled="isDisabled">Đăng kí</button>
                      </div>
                  </div>
                </div>
              </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import ld from 'lodash';
import { useStore } from "vuex";
import { useRouter } from "vue-router";

interface RegisterAccount {
  firstname: {
    value: string,
    error: string
  },
  lastname: {
    value: string,
    error: string
  },
  email: {
    value: string,
    error: string
  },
  username: {
    value: string,
    error: string
  },
  password: {
    value: string,
    error: string
  },
  confirmPassword: {
    value: string,
    error: string
  },
}
export default defineComponent({
  name: "signup-page",
  setup() {
    const store = useStore();
    const router = useRouter();
    const model = ref<RegisterAccount>({
      firstname: {
        error: '',
        value: ''
      },
      lastname: {
        error: '',
        value: ''
      },
      email: {
        error: '',
        value: ''
      },
      username: {
        error: '',
        value: ''
      },
      password: {
        error: '',
        value: ''
      },
      confirmPassword: {
        error: '',
        value: ''
      }
    });
    const isDisabled = ref<boolean>(false);
    const isReadOnly = ref<boolean>(false);
    const regexEmail = /^[a-z][a-z0-9_\.]{3,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/;
    const emailAsUsername = ref<boolean>(false);
    const registerAccount = async () => {
      const noErrors = Object.keys(model.value).every(x => ld.isEmpty(model.value[x]['error']));
      const noEmpty = Object.keys(model.value).every(x => !ld.isEmpty(model.value[x]['value']));

      if (noEmpty && noErrors) {
        isDisabled.value = true;
        const result = await store.dispatch('Register', {
          firstname: model.value.firstname.value,
          lastname: model.value.lastname.value,
          email: model.value.email.value,
          username: model.value.username.value,
          password: model.value.password.value,
          confirmPassword: model.value.confirmPassword.value,
        });

        if (result) {
          alert('Đăng kí thành công!');
          setTimeout(async () => {
            await router.push({name: 'login', path: '/login'});
          }, 1000);
          return;
        }
        isDisabled.value = false;
        alert('Đã xảy ra lỗi. Xin thử lại!');
      }
      else {
        alert('Còn tồn tại lỗi hoặc bạn chưa nhập đủ thông tin. Xin thử lại!');
      }
    }

    watch(() => emailAsUsername.value, (newData) => {
      isReadOnly.value = newData;
      if (newData) {
        model.value.username.value = model.value.email.value;
      }
      else {
        model.value.username.value = '';
      }
    })

    watch(() => model.value.email.value, (newData) => {
      if (!regexEmail.test(newData) || newData.length > 100) {
        model.value.email.error = 'Dữ liệu không hợp lệ';
      }
      else {
        model.value.email.error = '';
      }
      if (newData && emailAsUsername.value) {
        model.value.username.value = newData;
      }
    })

    watch(() => model.value.firstname.value, (newData) => {
      if (ld.isEmpty(newData) || newData.length > 100) {
        model.value.firstname.error = 'Dữ liệu không hợp lệ';
      }
      else {
        model.value.firstname.error = '';
      }
    })

    watch(() => model.value.lastname.value, (newData) => {
      if (ld.isEmpty(newData) || newData.length > 100) {
        model.value.lastname.error = 'Dữ liệu không hợp lệ';
      }
      else {
        model.value.lastname.error = '';
      }
    })

    watch(() => model.value.username.value, (newData) => {
      if (ld.isEmpty(newData) || newData.length > 100) {
        model.value.username.error = 'Dữ liệu không hợp lệ';
      }
      else {
        model.value.username.error = '';
      }
    })

    watch(() => model.value.password.value, (newData) => {
      model.value.confirmPassword.value = '';
      if (ld.isEmpty(newData) || newData.length > 20) {
        model.value.password.error = 'Dữ liệu không hợp lệ';
      }
      else {
        model.value.password.error = '';
      }
    })

    watch(() => model.value.confirmPassword.value, (newData) => {
      if (ld.isEmpty(newData) || newData.length > 20) {
        model.value.confirmPassword.error = 'Dữ liệu không hợp lệ';
      }
      else if (!ld.isEmpty(newData) && model.value.password.value.toLowerCase() !== newData.toLowerCase()) {
        model.value.confirmPassword.error = 'Mật khẩu không khớp';
      }
      else {
        model.value.confirmPassword.error = '';
      }
    })

    return {
      model,
      registerAccount,
      isDisabled,
      emailAsUsername,
      isReadOnly
    }
  }
});
</script>