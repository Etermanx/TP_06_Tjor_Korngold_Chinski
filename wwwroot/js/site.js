// Modificado de https://getbootstrap.com/docs/5.3/forms/validation/
function configurarValidacionFormsBoostrap() {
  const forms = document.querySelectorAll('.needs-validation');

  Array.from(forms).forEach(form => {
    form.addEventListener("submit", event => {
      if (!form.checkValidity()) {
        event.preventDefault();
        event.stopPropagation();
      }

      form.classList.add("was-validated");
    });
  });
}

function cambiarHeaderSticky() {
  const STICKY_CLASS = "sticky";
  const ALTURA_HEADER = 60;
  const headerElement = document.getElementsByTagName("header")[0];
  let distanciaStickyConseguida = this.scrollY >= ALTURA_HEADER;
  let stickyActivo = headerElement.classList.contains(STICKY_CLASS);
  

  if (distanciaStickyConseguida && !stickyActivo)
    headerElement.classList.add(STICKY_CLASS);
  else if (!distanciaStickyConseguida && stickyActivo)
    headerElement.classList.remove(STICKY_CLASS);
}


configurarValidacionFormsBoostrap();
window.addEventListener("scroll", cambiarHeaderSticky, false);