using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Elements.InteropServices
{
    /// <summary>The <see cref="GDI32" /> interoperability enables you to invoke unmanaged code.</summary>
    /// <remarks>
    ///     <para>Description: GDI Client DLL.</para>
    ///     <para>Entry point: <c>GDI32.dll</c></para>
    /// </remarks>
    [SuppressUnmanagedCodeSecurity]
    public static class GDI32
    {
        /// <summary>
        ///     Fills the specified buffer with the metrics for the currently selected font.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <param name="lptm">The LPTM.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetTextMetrics(HandleRef hDC, ref TEXTMETRIC lptm)
        {
            if (Marshal.SystemDefaultCharSize != 1)
            {
                // Handle Unicode
                return GetTextMetricsW(hDC, out lptm);
            }

            // Handle ANSI; call GetTextMetricsA and translate to Unicode struct
            TEXTMETRICA textMetricA;
            int result = GetTextMetricsA(hDC, out textMetricA);

            lptm.tmHeight = textMetricA.tmHeight;
            lptm.tmAscent = textMetricA.tmAscent;
            lptm.tmDescent = textMetricA.tmDescent;
            lptm.tmInternalLeading = textMetricA.tmInternalLeading;
            lptm.tmExternalLeading = textMetricA.tmExternalLeading;
            lptm.tmAveCharWidth = textMetricA.tmAveCharWidth;
            lptm.tmMaxCharWidth = textMetricA.tmMaxCharWidth;
            lptm.tmWeight = textMetricA.tmWeight;
            lptm.tmOverhang = textMetricA.tmOverhang;
            lptm.tmDigitizedAspectX = textMetricA.tmDigitizedAspectX;
            lptm.tmDigitizedAspectY = textMetricA.tmDigitizedAspectY;
            lptm.tmFirstChar = Convert.ToChar(textMetricA.tmFirstChar);
            lptm.tmLastChar = Convert.ToChar(textMetricA.tmLastChar);
            lptm.tmDefaultChar = Convert.ToChar(textMetricA.tmDefaultChar);
            lptm.tmBreakChar = Convert.ToChar(textMetricA.tmBreakChar);
            lptm.tmItalic = textMetricA.tmItalic;
            lptm.tmUnderlined = textMetricA.tmUnderlined;
            lptm.tmStruckOut = textMetricA.tmStruckOut;
            lptm.tmPitchAndFamily = textMetricA.tmPitchAndFamily;
            lptm.tmCharSet = textMetricA.tmCharSet;

            return result;
        }


        /// <summary>
        ///     Fills the specified buffer with the metrics for the currently selected font. This is the Ansi version of the
        ///     function.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <param name="lptm">The LPTM.</param>
        /// <returns>The <see cref="int" />.</returns>
        [DllImport(ExternalDLL.Gdi32, CharSet = CharSet.Ansi, ExactSpelling = false)]
        public static extern int GetTextMetricsA(HandleRef hDC, out TEXTMETRICA lptm);

        /// <summary>
        ///     Fills the specified buffer with the metrics for the currently selected font. This is the Unicode version of the
        ///     function.///
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <param name="lptm">The LPTM.</param>
        /// <returns>The <see cref="int" />.</returns>
        [DllImport(ExternalDLL.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = false)]
        public static extern int GetTextMetricsW(HandleRef hDC, out TEXTMETRIC lptm);

        /// <summary>
        ///     Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the
        ///     object. After the object has been deleted, the specified handle is no longer valid.
        /// </summary>
        /// <param name="hObject">The h object.</param>
        /// <returns>The <see cref="bool" />.</returns>
        [DllImport(ExternalDLL.Gdi32, CharSet = CharSet.Auto, ExactSpelling = false)]
        public static extern bool DeleteObject(HandleRef hObject);

        /// <summary>
        ///     Selects an object into the specified device context (DC). The new object replaces the previous object of the same
        ///     type.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <param name="hObject">The h object.</param>
        /// <returns>The <see cref="bool" />.</returns>
        [DllImport(ExternalDLL.Gdi32, CharSet = CharSet.Auto, ExactSpelling = false)]
        public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);


    }
}