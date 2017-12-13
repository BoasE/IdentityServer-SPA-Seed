<template>
    <div></div>
</template>

<script>
    import authService from './authService';

    export default {
        name: 'Auth',
        mounted() {
            authService.completeLogin();
        }
    };
</script>