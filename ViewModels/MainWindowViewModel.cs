using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Funktiot_Pinta_alat_jaTilavuudet.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{

    public decimal res1 { get; set; } = 0;
    public decimal res2 { get; set; } = 0;
    public event PropertyChangedEventHandler? PropertyChanged;
        
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public string[] muodot { get; } =
    {
        "Neliö",
        "Ympyrä",
        "Ympyrälieriö",
        "Pallo",
        "Kuutio",
        "Ympyrä kartio",
        "Neliöpyramidi"
    };
    

    public string lable1
    {
        get
        {
            switch (valitettuMuoto)
            {
                case 0:
                    return "Neliön sivu";
                case 1:
                    return "Ympyrän säde";
                case 2:
                    return "Ympyrälieriön säde";
                case 3:
                    return "Pallon säde";
                case 4:
                    return "Kuution sivu";
                case 5:
                    return "Ympyrä kartion säde";
                case 6:
                    return "Pyramidin pohjan sivu";
            }
            return "";
        }
    }
    
    public string lable2
    {
        get
        {
            switch (valitettuMuoto)
            {
                case 2:
                    return "Ympyrälieriön korkeus";
                case 5:
                    return "Ympyrä kartion korkeus";
                case 6:
                    return "Pyramidin pohjan korkeus";
            }
            return "";
        }
    }

    public bool tokaDataInputIsVisible
    {
        get
        {
            if (valitettuMuoto == 2 ||
                valitettuMuoto == 5 ||
                valitettuMuoto == 6) return true;
            return false;
        }
    }
    
    public bool tilavuusButtonIsVisible
    {
        get
        {
            if (valitettuMuoto == 2 ||
                valitettuMuoto == 3 ||
                valitettuMuoto == 4 ||
                valitettuMuoto == 5 ||
                valitettuMuoto == 6) return true;
            return false;
        }
        
    }
    
    public bool tokaButtonIsVisible
    {
        get
        {
            if (valitettuMuoto == 2 ||
                valitettuMuoto == 5 ||
                valitettuMuoto == 6) return true;
            return false;
        }
        
    }
    
    
    public decimal ekaValue {get; set;}
    public decimal tokaValue {get; set;}
    public int _valitettuMuoto = 0;

    public int valitettuMuoto
    {
        get => _valitettuMuoto;
        set
        {
            if (_valitettuMuoto == value) return;
            _valitettuMuoto = value;
            RaisePropertyChanged(nameof(lable1));
            RaisePropertyChanged(nameof(lable2));
            RaisePropertyChanged(nameof(tokaDataInputIsVisible));
            RaisePropertyChanged(nameof(tilavuusButtonIsVisible));
            
        }
    }

    public void laskePinta()
    {
         Creator creator = new Creator();
         var kuva = creator.FactoryMethod(valitettuMuoto, ekaValue,tokaValue);
         res1 = kuva.laskePintaAla();
         RaisePropertyChanged(nameof(res1));
    }

    public void laskeTilavuus()
    {
        Creator creator = new Creator();
        var kuva = creator.FactoryMethod(valitettuMuoto, ekaValue,tokaValue);
        res2 = kuva.laskeTilavuus();
        RaisePropertyChanged(nameof(res2));
    }
}

// interface tasoLuvut
// {
//     public decimal laskePintaAla();
// }
//
// interface tilaLuvut
// {
//     public decimal laskePintaAla();
//     public decimal laskeTilavuus();
// }
//

public class Nelio: geometrinenKuvio
{
    private decimal sivu = 0;

    public Nelio(decimal sivuI)
    {
        sivu = sivuI;
    }

    public override decimal laskePintaAla()
    {
        return sivu * sivu;
    }

    public override decimal laskeTilavuus()
    {
        return 0;
    }
}

public class Ympyra: geometrinenKuvio
{
    private decimal sade = 0;

    public Ympyra(decimal sadeI)
    {
        sade = sadeI;
    }

    public override decimal laskePintaAla()
    {
        return sade * sade * (decimal)Math.PI;
    }

    public override decimal laskeTilavuus()
    {
        return 0;
    }
}

public class Ympyralierio: geometrinenKuvio
{
    private decimal sade = 0;
    private decimal korkeus = 0;

    public Ympyralierio(decimal sadeI, decimal korkeusI)
    {
        sade = sadeI;
        korkeus = korkeusI;
    }

    public override decimal laskePintaAla()
    {
        return 2 * sade * sade * (decimal)Math.PI +    //2 pohjan pinta 
               2 * (decimal)Math.PI * sade * korkeus ; //sivun pinta
    }

    public override decimal laskeTilavuus()
    {
        return sade * sade * (decimal)Math.PI * korkeus ;
    }
}

public class Pallo : geometrinenKuvio
{
    private decimal sade = 0;
    
    public Pallo(decimal sadeI)
    {
        sade = sadeI;
    }

    public override decimal laskePintaAla()
    {
        return sade * sade * (decimal)Math.PI * 4 ;
    }

    public override decimal laskeTilavuus()
    {
        return sade * sade * sade * (decimal)Math.PI * 4 / 3;
    }
}

public class Kuutio : geometrinenKuvio
{
    private decimal sivu = 0;
    public Kuutio(decimal sivuI)
    {
        sivu = sivuI;
    }
    public override decimal laskePintaAla()
    {
        return sivu * sivu * 6;
    }

    public override decimal laskeTilavuus()
    {
        return sivu * sivu * sivu;
    }
}

public class YmpyraKartio : geometrinenKuvio
{
    private decimal korkeus = 0;
    private decimal sade = 0;
    private decimal t = 0;
    public YmpyraKartio(decimal sadeI, decimal korkeusI)
    {
        korkeus = korkeusI;
        sade = sadeI;
    }
    public override decimal laskePintaAla()
    {
        t = (decimal)Math.Sqrt((double)(sade * sade + korkeus * korkeus));
        return (decimal)Math.PI * sade * sade + //pohjan pinta
               (decimal)Math.PI * sade * t;     //sivun pinta
    }

    public override decimal laskeTilavuus()
    {
        
        return (decimal)Math.PI * sade * sade * korkeus / 3;
    }
}

public class Pyramidi : geometrinenKuvio
{
    private decimal korkeus = 0;
    private decimal pohjan_sivu = 0;
    private decimal ht = 0;

    public Pyramidi(decimal pohjan_sivuI, decimal korkeusI)
    {
        korkeus = korkeusI;
        pohjan_sivu = pohjan_sivuI;
    }
    public override decimal laskePintaAla()
    {
        ht = (decimal)Math.Sqrt(
            (double)(korkeus * korkeus + 
                     (decimal)Math.Pow((double)(pohjan_sivu / 2), 2))
            );
        return pohjan_sivu * pohjan_sivu + //pohjan ala
               2 * pohjan_sivu * ht;       //sivu ala
    }

    public override decimal laskeTilavuus()
    {
        return korkeus * pohjan_sivu * pohjan_sivu / 3;
    }
}

public abstract class geometrinenKuvio
{
    public abstract decimal laskePintaAla();
    public abstract decimal laskeTilavuus();
}

class Creator
{
    public geometrinenKuvio FactoryMethod(
        int muoto,
        decimal data1,
        decimal data2 = 0)
    {
        switch (muoto)
        {
            case 0:
            return new Nelio(data1);
            case 1:
            return new Ympyra(data1);
            case 2:
            return new Ympyralierio(data1, data2);
            case 3:
            return new Pallo(data1);
            case 4:
            return new Kuutio(data1);
            case 5:
            return new YmpyraKartio(data1, data2);
            default:
            return new Pyramidi(data1, data2);
        }
    }
    
}