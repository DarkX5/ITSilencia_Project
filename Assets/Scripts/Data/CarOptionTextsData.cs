using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Car Option Texts Data")]
public class CarOptionTextsData : ScriptableObject {
    [SerializeField] private string language = "en";
    [SerializeField] private DriveOptionText[] driveOptionTexts = {
        new DriveOptionText(CarDriveOption.T3Manual160KM, "T3 Manual (160 KM)"),
        new DriveOptionText(CarDriveOption.T3Automatic160KM, "T3 Automatic (160 KM)"),
        new DriveOptionText(CarDriveOption.B4MildHybrid190KM, "B4 Mild Hybrid (190 KM)"),
        new DriveOptionText(CarDriveOption.B4AWDMildHybrid190KM, "B4 AWD Mild Hybrid (190 KM)"),
        new DriveOptionText(CarDriveOption.B4AWDMildHybrid250KM, "B4 AWD Mild Hybrid (250 KM)")
    };
    [SerializeField]
    private ColorOptionText[] colorOptionTexts = {
        new ColorOptionText(CarColorOption.blackStone, "Black Stone"),
        new ColorOptionText(CarColorOption.denimBlue, "Denim Blue"),
        new ColorOptionText(CarColorOption.fusionRed, "Fusion Red"),
        new ColorOptionText(CarColorOption.glacierSilver, "Glacier Silver"),
        new ColorOptionText(CarColorOption.iceWhite, "Ice White"),
        new ColorOptionText(CarColorOption.itsGreen, "Its Green")
    };
    [SerializeField]
    private UpholsteryOptionText[] upholsteryOptionTexts = {
        new UpholsteryOptionText(CarUpholsteryOption.black, "Black"),
        new UpholsteryOptionText(CarUpholsteryOption.white, "White")
    };
    [SerializeField]
    private PackagesOptionText[] packageOptionTexts = {
        new PackagesOptionText(CarPackagesOption.lighting, "Lighting"),
        new PackagesOptionText(CarPackagesOption.parking, "Parking"),
        new PackagesOptionText(CarPackagesOption.technology, "Technology"),
        new PackagesOptionText(CarPackagesOption.winter, "Winter")
    };

    public DriveOptionText[] DriveOptionsTexts { get { return driveOptionTexts; } }
    public ColorOptionText[] ColorOptionsTexts { get { return colorOptionTexts; } }
    public UpholsteryOptionText[] UpholsteryOptionsTexts { get { return upholsteryOptionTexts; } }
    public PackagesOptionText[] PackageOptionsTexts { get { return packageOptionTexts; } } 
}

// define Option Text 
public class OptionText
{
    public string text;
}

// define Drive Options
public enum CarDriveOption
{
    T3Manual160KM,
    T3Automatic160KM,
    B4MildHybrid190KM,
    B4AWDMildHybrid190KM,
    B4AWDMildHybrid250KM
}
[Serializable]
public class DriveOptionText : OptionText {
    public DriveOptionText(CarDriveOption newDriveOptions, string newText) {
        this.carDriveOption = newDriveOptions;
        text = newText;
    }
    public CarDriveOption carDriveOption;
}

// define Color Options
public enum CarColorOption
{
    blackStone,
    iceWhite,
    glacierSilver,
    denimBlue,
    fusionRed,
    itsGreen
}
[Serializable]
public class ColorOptionText : OptionText
{
    public ColorOptionText(CarColorOption newColorOption, string newText)
    {
        this.carColorOption = newColorOption;
        text = newText;
    }
    public CarColorOption carColorOption;
}

// define Upholstery Options
public enum CarUpholsteryOption
{
    black,
    white
}
[Serializable]
public class UpholsteryOptionText : OptionText
{
    public UpholsteryOptionText(CarUpholsteryOption newUpholsteryOptions, string newText)
    {
        this.carUpholsteryOption = newUpholsteryOptions;
        text = newText;
    }
    public CarUpholsteryOption carUpholsteryOption;
}

// define Packages Options
public enum CarPackagesOption
{
    winter,
    parking,
    technology,
    lighting
}
[Serializable]
public class PackagesOptionText : OptionText
{
    public PackagesOptionText(CarPackagesOption newPackagesOptions, string newText)
    {
        this.carPackagesOption = newPackagesOptions;
        text = newText;
    }
    public CarPackagesOption carPackagesOption;
}