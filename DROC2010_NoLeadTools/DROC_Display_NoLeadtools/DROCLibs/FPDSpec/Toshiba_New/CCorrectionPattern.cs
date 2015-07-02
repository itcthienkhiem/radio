//***********************************************************************************************************
/*!
 * @file		CCorrectionPattern.cs
 * @brief		Correction pattern class.
 * 
 * @author		TJ
 * @date		2012/03/30(Fri) 
 */
//***********************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VietBaIT.DROC
{
    //*******************************************************************************************************
    /*!
     * @class		Correction pattern class.
     * @brief		Keap correction pattern in Boolean flag.
     * 
     * @author		TJ
     * @date		2012/03/30(Fri) 
     */
    //*******************************************************************************************************
    public class CCorrectionPattern
    {
        #region private fields
        // Offset correction A bit pattern
        private const Byte OFFSETA_BIT = 0x01;
        // Offset correction B bit pattern
        private const Byte OFFSETB_BIT = 0x02;
        // Gain correction bit pattern
        private const Byte GAIN_BIT = 0x04;
        // Defect correction bit pattern
        private const Byte DEFECT_BIT = 0x08;
        #endregion

        #region public fields
        // Offset correction A boolan flag
        public Boolean offsetA { get; private set; }
        // Offset correction B boolan flag
        public Boolean offsetB { get; private set; }
        // Gain correction boolan flag
        public Boolean gain { get; private set; }
        // Defect correction boolan flag
        public Boolean defect { get; private set; }
        #endregion

        #region constructor
        //***************************************************************************************************
        /*!
         * @brief		Constructor (from bit pattern).
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 correctPattern   Correction bit pattern.
         * 
         * @return		none
         */
        //***************************************************************************************************
        public CCorrectionPattern(UInt32 correctPattern)
        {
            // Initialize
            this.offsetA = false;
            this.offsetB = false;
            this.gain = false;
            this.defect = false;

            // Check correction bit pattern
            if (OFFSETA_BIT == (correctPattern & OFFSETA_BIT))
            {
                this.offsetA = true;
            }
            if (OFFSETB_BIT == (correctPattern & OFFSETB_BIT))
            {
                this.offsetB = true;
            }
            if (GAIN_BIT == (correctPattern & GAIN_BIT))
            {
                this.gain = true;
            }
            if (DEFECT_BIT == (correctPattern & DEFECT_BIT))
            {
                this.defect = true;
            }
        }

        //***************************************************************************************************
        /*!
         * @brief		Constructor (from boolean flag).
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   Boolean offsetA     Offset correction A boolan flag.
         * @param[in]   Boolean offsetB     Offset correction B boolan flag.
         * @param[in]   Boolean gain        Gain correction boolan flag.
         * @param[in]   Boolean defect      Defect correction boolan flag.
         * 
         * @return		none
         */
        //***************************************************************************************************
        public CCorrectionPattern(Boolean offsetA, Boolean offsetB, Boolean gain, Boolean defect)
        {
            // Set boolean flag
            this.offsetA = offsetA;
            this.offsetB = offsetB;
            this.gain = gain;
            this.defect = defect;
        }
        #endregion

        #region public methods
        //***************************************************************************************************
        /*!
         * @brief		Returns Correction bit pattern.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         * 
         * @return		Correction bit pattern.
         */
        //***************************************************************************************************
        public UInt32 GetFlag()
        {
            UInt32 correctFlag = 0;

            // Make correction bit pattern
            if (true == this.offsetA)
            {
                correctFlag |= OFFSETA_BIT;
            }
            if (true == this.offsetB)
            {
                correctFlag |= OFFSETB_BIT;
            }
            if (true == this.gain)
            {
                correctFlag |= GAIN_BIT;
            }
            if (true == this.defect)
            {
                correctFlag |= DEFECT_BIT;
            }

            return correctFlag;
        }
        #endregion
    }
}
