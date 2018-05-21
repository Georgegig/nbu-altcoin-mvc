'use strict';

let NavigationComponent = Vue.component('nav-component', {
    template: `
    <v-toolbar class="white">          
        <v-btn flat href="/"><h1 style="cursor:pointer;" class="title" v-text="title"></h1></v-btn>
        <v-spacer></v-spacer>
        <v-toolbar-side-icon class="hidden-md-and-up"></v-toolbar-side-icon>
        <span><h1 v-text="loggedUser"></h1></span>
        <v-toolbar-items class="hidden-sm-and-down">
            <v-btn flat v-show="userLoggedIn" href="/portfolio">Portfolio</v-btn>
            <v-btn flat v-show="userLoggedIn" href="/users/logout">Log out</v-btn>
            <v-btn flat v-show="!userLoggedIn" href="/users/login">Log in</v-btn>
            <v-btn flat v-show="!userLoggedIn" href="/users/register">Register</v-btn>
        </v-toolbar-items>
    </v-toolbar>`,
    props: ['title'],
    data() {
        return {
            userLoggedIn: false,
            loggedUser: ''
        }
    },
    mounted() {
        this.loginStatus();
        this.$eventHub.$on('loginChange', () => {
            this.loginStatus();
        });
    },
    methods: {
        loginStatus() {
            this.userLoggedIn = UsersTable.userLoggedIn();
            if (this.userLoggedIn) {
                this.loggedUser = UsersTable.getLoggedUserMail();
            }
            else {
                this.loggedUser = '';
            }
        }
    }
});
