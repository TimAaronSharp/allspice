import { logger } from "@/utils/Logger.js";
import { Pop } from "@/utils/Pop.js";

export function validateForm(event){
const formElem = event.target
    /** @type {NodeListOf<HTMLInputElement | HTMLTextAreaElement>} */
    const inputs = formElem.querySelectorAll('input, textarea')

    for(const input of inputs){
      if (input.value.trim() === '') {
        event.preventDefault();
        Pop.toast('A comment cannot be only whitespace. Please enter a comment to post.')
        input.focus()
        return input.value.trim()
      }
    }
    if (!formElem.checkValidity()) {
      event.preventDefault()
      formElem.reportValidity()
    }
    // NOTE Will need to rewrite this for forms that are more than one input. Return inputs maybe?
    return formElem[0]
  
  
}