import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { AuthComponent } from './auth/auth.component';
import { ErrorComponent } from './error/error.component';
import { CategoryTreeComponent } from './category-tree/category-tree.component';
import { PaginationComponent } from './pagination/pagination.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { CustomDatePipeEn } from '../core/pipes/custom-date-en.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShopCardComponent } from './shop-card/shop-card.component';
import { CategoryCardComponent } from './category-card/category-card.component';
import { ShortNumberPipe } from '../core/pipes/short-number.pipe';
import { DateOnlyPipe } from '../core/pipes/date-only.pipe';
import { PhoneNumberPipe } from '../core/pipes/phone-number.pipe';
import { SpinnerComponent } from './spinner/spinner.component';
import { ProductLoadingCardComponent } from './product-loading-card/product-loading-card.component';
import { ShopLoadingCardComponent } from './shop-loading-card/shop-loading-card.component';
import { CategoryLoadingCardComponent } from './category-loading-card/category-loading-card.component';
import { BooleanToYesNoPipe } from '../core/pipes/boolean-to-yes-no.pipe';
import { ChildOrAdultPipe } from '../core/pipes/child-or-adult.pipe';
import { TimeSpanToHoursMinutesPipe } from '../core/pipes/timespan-to-time.pipe';
import { DateToTimePipe } from '../core/pipes/date-to-time.pipe';
import { ChatDatePipeEn } from '../core/pipes/chat-date-en.pipe';
import { MessageToastComponent } from './message-toast/message-toast.component';

@NgModule({
  declarations: [
    AuthComponent,
    BooleanToYesNoPipe,
    CategoryCardComponent,
    CategoryLoadingCardComponent,
    CategoryTreeComponent,
    ChatDatePipeEn,
    ChildOrAdultPipe,
    CustomDatePipeEn,
    DateOnlyPipe,
    DateToTimePipe,
    ErrorComponent,
    FooterComponent,
    HeaderComponent,
    MessageToastComponent,
    PaginationComponent,
    PhoneNumberPipe,
    ProductCardComponent,
    ProductLoadingCardComponent,
    ShopCardComponent,
    ShopLoadingCardComponent,
    ShortNumberPipe,
    SpinnerComponent,
    TimeSpanToHoursMinutesPipe
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  exports: [
    BooleanToYesNoPipe,
    CategoryCardComponent,
    CategoryLoadingCardComponent,
    ChatDatePipeEn,
    ChildOrAdultPipe,
    CustomDatePipeEn,
    DateOnlyPipe,
    DateToTimePipe,
    ErrorComponent,
    FooterComponent,
    HeaderComponent,
    MessageToastComponent,
    PaginationComponent,
    PhoneNumberPipe,
    ProductCardComponent,
    ProductLoadingCardComponent,
    ShopCardComponent,
    ShopLoadingCardComponent,
    ShortNumberPipe,
    SpinnerComponent,
    TimeSpanToHoursMinutesPipe
  ],
  providers: [PaginationComponent]
})
export class SharedModule { }