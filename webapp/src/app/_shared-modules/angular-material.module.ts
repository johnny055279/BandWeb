import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatCommonModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatBadgeModule } from '@angular/material/badge';
@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        MatFormFieldModule,
        MatCommonModule,
        MatToolbarModule,
        MatIconModule,
        MatButtonModule,
        MatSidenavModule,
        MatListModule,
        MatGridListModule,
        MatCardModule,
        MatProgressSpinnerModule,
        MatStepperModule,
        MatSelectModule,
        MatInputModule,
        MatTooltipModule,
        MatProgressBarModule,
        MatSnackBarModule,
        MatDialogModule,
        MatBadgeModule
    ],
    exports: [
        MatCommonModule,
        MatFormFieldModule,
        MatToolbarModule,
        MatIconModule,
        MatButtonModule,
        MatSidenavModule,
        MatListModule,
        MatGridListModule,
        MatCardModule,
        MatProgressSpinnerModule,
        MatStepperModule,
        MatSelectModule,
        MatInputModule,
        MatTooltipModule,
        MatProgressBarModule,
        MatSnackBarModule,
        MatDialogModule,
        MatBadgeModule
    ]
})
export class AngularMaterialModule { }
