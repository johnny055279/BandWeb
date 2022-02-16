import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Directive({
    selector: '[appCheckRole]'
})
export class CheckRoleDirective implements OnInit {
    @Input() appCheckRole!: string[];
    user?: User;
    constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>, private accountService: AccountService) {
        this.accountService.account$.pipe(take(1)).subscribe(user => {
            this.user = user;
        })
    }
    ngOnInit(): void {
        console.log(this.appCheckRole)
        if (!this.user?.roles || this.user == null) {
            this.viewContainerRef.clear();
            return;
        }

        if (this.user?.roles.some(role => this.appCheckRole.includes(role))) {
            this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
            this.viewContainerRef.clear();
        }
    }



}
