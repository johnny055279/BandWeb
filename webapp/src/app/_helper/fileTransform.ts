import { SafeUrl } from "@angular/platform-browser";

export class FileTransform {

    Base64ToBlob(b64Data: string, contentType: string, sliceSize: number) {
        const byteCharacters = atob(b64Data);
        const byteArrays = [];
        for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            const slice = byteCharacters.slice(offset, offset + sliceSize);
            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }
            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }
        const blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }

    FileToBase64(component: any, file: any, GetBase64: any) {
        let reader = new FileReader();
        let q: Array<string> = [];
        reader.readAsDataURL(file);
        reader.onload = function () {
            q = reader.result?.toString().split(',') || [];
            GetBase64(component, q[1]);
        }
    }
}
