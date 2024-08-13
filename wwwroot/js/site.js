// Modificado de https://getbootstrap.com/docs/5.3/forms/validation/
function configurarValidacionFormsBoostrap() {
  const forms = document.querySelectorAll('.needs-validation')

  Array.from(forms).forEach(form => {
    form.addEventListener('submit', event => {
      if (!form.checkValidity()) {
        event.preventDefault()
        event.stopPropagation()
      }
    });
  });

  form.classList.add("was-validated");
}

function cambiarHeaderSticky() {
  const STICKY_CLASS = "sticky";
  const ALTURA_HEADER = 60;
  const navElement = document.getElementsByTagName("nav")[0];
  let distanciaStickyConseguida = this.scrollY > ALTURA_HEADER;
  let stickyActivo = navElement.classList.contains(STICKY_CLASS);
  

  if (distanciaStickyConseguida && !stickyActivo)
    navElement.classList.add(STICKY_CLASS);
  else if (!distanciaStickyConseguida && stickyActivo)
    navElement.classList.remove(STICKY_CLASS);
}


configurarValidacionFormsBoostrap();
window.addEventListener("scroll", cambiarHeaderSticky, false);