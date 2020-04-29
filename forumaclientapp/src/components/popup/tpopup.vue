<template name="component-name">
  <div class="t-popup container">
    <div class="t-popup-header justify-content-end ">
      <div>
        <button class="btn btn-danger" @click="closePupup">Close</button>
      </div>
    </div>
    <div class="t-popup-content">
      <slot> </slot>
    </div>

    <div class="t-popup-footer"></div>
  </div>
</template>

<script>
export default {
  name: "tpopup",

  methods: {
    closePupup() {
      this.$emit("closePopup");
    }
  }
};
</script>

<style lang="scss">
.closeicon {
  cursor: pointer;
}

.t-popup {
  padding: 5px;
  position: fixed;

  width: 400px;
  background: #c0c0c0;

  &__header,
  &__footer {
    display: Flex;
    justify-content: space-between;
    align-items: flex-end;
  }
  &__content {
    display: flex;
    justify-content: center;
    align-items: center;
  }
}
</style>
