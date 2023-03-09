<template>
    <div class="container-fluid none-padding">
      <div class="row">
          <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 user-information">
            <h3>Xin Chào, {{ user.fullName }}</h3>
            <button class="btn btn-default" @click="logout">Thoát</button>
          </div>
      </div>
      <div class="row">
          <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 wrapper">
            <div class="scroll-container" @scroll="scrollElements">
              <div class="movie-item" v-for="item in movies" :style="`background-image: url(${item?.imageUrl})`">
                <div class="row movie-information">
                  <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10 col-xl-10 col-xxl-10">
                    <p class="title">{{ item?.title }}</p>
                    <span class="description">Lorem, ipsum dolor sit amet consectetur adipisicing elit. Architecto reiciendis soluta odit necessitatibus error. Suscipit!</span>
                  </div>
                  <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2 col-xl-2 col-xxl-2 text-center">
                    <i :id="item?.id" :class="`fas fa-heart none-like ${item?.isLiked ? `liked` : ``}`" @click="likeAction(item?.id)"></i>
                    <span class="num-of-like">{{ item?.likeNo }}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
      </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed } from "vue";
import { useStore } from "vuex";
import ld from 'lodash'
import {ActionType} from '@/core/enum'
interface MovieInformation {
  title: string,
  imageUrl: string,
  likeNo: number,
  isLiked: boolean,
  id: string
}

export default defineComponent({
  name: "index-page",
  setup () {
    const store = useStore();
    const currentPage = ref<number>(1);
    const user = computed(() =>{
      return store.getters.getUser
    });

    const movies = computed<MovieInformation[]>(() =>{
      return store.getters.getMovies
    });

    const model = ref<MovieInformation>(movies.value[0])

    const logout = async () => {
      const choose = confirm("Bạn muốn thoát khỏi hệ thống ?");
      if (choose) {
        await store.dispatch('Logout');
        window.location.href = '/login';
      }
    }

    const likeAction = async (id:string) => {
      const item = document.getElementById(`${id}`);

      if (!ld.isNil(item)) {
        const isLiked = item!.classList.contains("liked");
        
        item!.classList.toggle("liked");
        const result = await store.dispatch('LikeMovie', {
          movieId: id,
          actionType: isLiked ? ActionType.Descrease : ActionType.Increase
        });
      }
    }

    const scrollElements = async (e) => {
      if (Math.round(e.target.offsetHeight + e.target.scrollTop) >= e.target.scrollHeight)
      {
        await store.dispatch('GetMovies', {
          pageIndex: currentPage.value++
        });
      }
    }
    return {
      user,
      logout,
      likeAction,
      model,
      movies,
      scrollElements
    }
  }
});
</script>