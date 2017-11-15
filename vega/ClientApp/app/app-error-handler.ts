
import * as Raven from 'raven-js';
import { Inject, ErrorHandler, NgZone, isDevMode } from "@angular/core";
import { ToastyService } from "ng2-toasty";

export class AppErrorHandler implements ErrorHandler {

    constructor( @Inject(NgZone) private ngZone: NgZone,
        @Inject(ToastyService) private toastService: ToastyService) {

    }

    handleError(error: any): void {

        this.ngZone.run(() => {
            if (typeof (window) !== 'undefined') {
                this.toastService.error({
                    title: 'Error',
                    msg: 'An unexpected error ocurred',
                    theme: 'bootstrap',
                    showClose: true,
                    timeout: 5000
                });
            }
        });

        if (!isDevMode())
            Raven.captureException(error.originalError || error);
        else
            throw error;

       
    }



}