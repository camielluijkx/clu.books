define(['durandal/system'], function (system) {
    var logger = {
        log: log,
        logError: logError,
        logWarning: logWarning,
        logSuccess: logSuccess
    };

    return logger;

    function log(message, data, source, showToast) {
        logIt(message, data, source, showToast, 'info');
    }

    function logError(message, data, source, showToast) {
        logIt(message, data, source, showToast, 'error');
    }

    function logWarning(message, data, source, showToast) {
        logIt(message, data, source, showToast, 'warning');
    }

    function logSuccess(message, data, source, showToast) {
        logIt(message, data, source, showToast, 'success');
    }

    function logIt(message, data, source, showToast, toastType) {
        source = source ? '[' + source + '] ' : '';
        if (data) {
            system.log(source, message, data);
        } else {
            system.log(source, message);
        }
        if (showToast) {
            if (toastType === 'error') {
                toastr.error(message);
            } else if (toastType === 'warning') {
                toastr.warning(message);
            } else if (toastType === 'success') {
                toastr.success(message);
            } else {
                toastr.info(message);
            }
        }
    }
});