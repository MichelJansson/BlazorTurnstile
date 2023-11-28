
export function render(obj, element, parameters) {
    parameters['callback'] = (token) => obj.invokeMethodAsync('Callback', token);
    parameters['error-callback'] = (message) => obj.invokeMethodAsync('ErrorCallback', message);

    // Convert non-callback parameter(s) with values containing "_" to expected values with "-" because deserliations do not respect enumerations.
    for (let key in parameters) {
        if (!["call-back", "action", "cData"].includes(key)) {
            if (parameters[key] && typeof parameters[key] === 'string' && parameters[key].includes("_")) {
                parameters[key] = parameters[key].replace(/_/g, "-");
            }
        }
    }
    turnstile.render(element, parameters);
}

export function reset() {
    turnstile.reset();
}
