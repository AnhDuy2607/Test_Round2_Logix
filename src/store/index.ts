import { createStore } from "vuex";
import createPersistedState from "vuex-persistedstate";
import { config, Module, Action, Mutation, } from "vuex-module-decorators";
import axios from "axios";
import { ActionType } from "@/core/enum";
config.rawError = true;

const setHeader = (token) => {
  axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
}
const store = createStore({
  state () {
    return {
      user: {},
      token: "",
      movies: [] as any[]
    }
  },
  getters: {
    getToken(state) {
      return state.token;
    },
    getUser(state) {
      return state.user;
    },
    getMovies(state) {
      return state.movies;
    }
  },
  mutations: {
    setToken(state, payload:any) {
      state.token = payload.data.token;
    },
    clearData(state) {
      state.token = "";
      state.user = {};
      state.movies = [];
    },
    setUser(state, payload:any) {
      state.user = payload.data.user;
    },
    setMovies(state, payload:any) {
      state.movies = state.movies.concat(payload.data);
    },
    updateLikeNo(state, payload) {
      const item = state.movies.find(x => x.id === payload.data.movieId);

      if (item) {
        item.likeNo = payload.data.likeNo
      }
    }
  },
  actions: {
    Login(context, credentials:any) {
      return axios.post("/accounts/login", credentials)
      .then(({ data }) => {
          context.commit("setUser", data);
          context.commit("setToken", data);
      });
    },
    Logout(context) {
      setHeader(context.state.token);
      axios.put("/accounts/logout")
        .then(() => {
           context.commit("clearData");
        });
    },
    Register(context, credentials:any) {
      return axios.post("/accounts/newAccount", credentials)
        .then(({ data }) => {
            return data !== null
        });
    },
    GetMovies(context, payload) {
      setHeader(context.state.token);
      axios.get(`/movies/${payload.pageIndex}`)
        .then(({data}) => {
           context.commit("setMovies", data);
        });
    },
    LikeMovie(context, payload) {
      setHeader(context.state.token);
      return axios.put(`/movies/${payload.movieId}/${payload.actionType}`)
        .then(({data}) => {
          if (data) {
            context.commit("updateLikeNo", data);
          }
          return data;
        })
    }
  },
  plugins: [
    createPersistedState({
      key: "store_values",
      storage: window.sessionStorage
    }),
  ],
});

export default store;
