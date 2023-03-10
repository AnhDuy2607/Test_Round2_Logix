<style scoped lang="scss">
.login-form{
    width: 100%;
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    .login-information{
        width: 35%;
        .username-information{
            display: flex;
            flex-direction: column;
        }
        .pwd-information{
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
    .login-information{
        width: 100%!important;
    }
}
</style>

<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 login-form">
                <div class="card login-information">
                    <div class="card-header text-center"><h4>Đăng Nhập Hệ Thống</h4></div>
                    <div class="card-body">
                        <div class="mb-3 row username-information">
                            <label for="inputPassword" class="col-sm-12 col-form-label">Tên đăng nhập:</label>
                            <div class="col-sm-12">
                                <input type="text" class="form-control" id="inputUsername" maxlength="100" v-model.trim="loginModel.username" :disabled="isDisabled" placeholder="Tên đăng nhập/ email">
                            </div>
                        </div>
                        <div class="mb-3 row pwd-information">
                            <label for="inputPassword" class="col-sm-12 col-form-label">Mật khẩu:</label>
                            <div class="col-sm-12">
                                <input type="password" class="form-control" id="inputPassword" maxlength="20" v-model.trim="loginModel.password" :disabled="isDisabled">
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="row action-form">
                            <div class="col-sm-12">
                                <router-link to="/signup">Đăng kí mới</router-link>
                                <button class="btn btn-primary" @click="login" :disabled="isDisabled">Đăng nhập</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { useStore } from "vuex";
import lodash from 'lodash';
import { useRouter } from "vue-router";

interface LoginInformation {
    username: string,
    password: string
}
export default defineComponent({
  name: "login-page",
  setup () {
    const store = useStore();
    const router = useRouter();
    const loginModel = ref<LoginInformation>({
        username: '',
        password: ''
    });

    const isDisabled = ref<boolean>(false);

    const login = async () => {
        if (lodash.isNil(loginModel.value) || lodash.isEmpty(loginModel.value.username) || lodash.isEmpty(loginModel.value.password)) {
            alert('Xin mời bạn nhập đầy đủ thông tin.');
            return;
        }

        try {
            isDisabled.value = true;
            await store.dispatch("Login", loginModel.value);
            setTimeout(async () => {
                await router.push({name: 'home', path: '/'});
            }, 1000);
        }
        catch (ex) {
            isDisabled.value = false;
            alert('Đã xảy ra lỗi xin vui lòng thử lại!');
        }
    }
    return {
        loginModel,
        login,
        isDisabled
    }
  }
});
</script>