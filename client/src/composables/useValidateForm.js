import { logger } from "@/utils/Logger.js";

export function validateForm(formElem){
  const formElemTest = document.getElementById(formElem).addEventListener('submit', function (event) {
    logger.log("formElemTest is ", formElemTest)
    /** @type {NodeListOf<HTMLInputElement | HTMLTextAreaElement>} */
    const inputs = formElem.querySelectorAll('input, textarea')

    for(const input of inputs){
      if (input.value.trim() === '') {
        event.preventDefault();
        alert('A comment cannot be only whitespace. Please enter a comment to post.')
        input.focus()
        return
      }
    }
    if (!formElem.checkValidity()) {
      event.preventDefault()
      formElem.reportValidity()
    }
  })
  logger.log("formElemTest is ", formElemTest)
}