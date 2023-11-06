import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpStatusCode } from 'axios';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { EmailVerifyCredential } from '../../models/auth-credentials';
import { AuthFacadeService } from '../../services/auth-facade.service';

@Component({
  selector: 'app-email-verify',
  templateUrl: './email-verify.component.html',
  styleUrls: ['./email-verify.component.scss']
})
export class EmailVerifyComponent implements OnInit {

  userId: string | null = null;
  token: string | null = null;
  emailVerifySuccess: boolean = false;
  emailVerifyUnsuccess: boolean = false;
  regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;

  constructor(private authFacade: AuthFacadeService, private router: Router, private route: ActivatedRoute, private spinnerService: SpinnerService) { }

  async ngOnInit(): Promise<void> {
    try {
      this.userId = this.route.snapshot.queryParamMap.get('userid');
      this.token = this.route.snapshot.queryParamMap.get('token');
      
      if (!this.token) {
        this.router.navigateByUrl('/404', { skipLocationChange: true });
        return;
      }
      
      if(!this.userId || !this.regex.test(this.userId)) {
        this.router.navigateByUrl('/404', { skipLocationChange: true });
        return;
      }

      if(this.userId && this.token) {
        this.spinnerService.changeSpinnerState(true);
        const emailVerifyCredential: EmailVerifyCredential = {
          userId: this.userId,
          token: this.token,
        };
      
        await this.authFacade.emailVerify(emailVerifyCredential);
        this.spinnerService.changeSpinnerState(false);
        this.emailVerifyUnsuccess = false;
        this.emailVerifySuccess = true;
      }
    } catch(error : any) {
      this.spinnerService.changeSpinnerState(false);
      if(error.response.status === HttpStatusCode.NotFound) this.router.navigateByUrl('/404', { skipLocationChange: true });
      else if(error.response.status === HttpStatusCode.BadRequest) this.emailVerifyUnsuccess = true;
      else if(error.response.status === HttpStatusCode.Unauthorized) this.router.navigate(["/"]);
    }
  }

  backToLogin(): void { this.router.navigate(["/auth/login"]); }
}
