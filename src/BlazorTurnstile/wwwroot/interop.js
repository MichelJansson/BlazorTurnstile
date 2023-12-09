export function render(obj, element, parameters) {
    parameters['callback'] = (token) => obj.invokeMethodAsync('Callback', token);
    parameters['error-callback'] = (message) => obj.invokeMethodAsync('ErrorCallback', message);

    return turnstile.render(element, parameters);
}
