﻿using System;

namespace SemanticTypes.TypeSystem.Physics;

/// <summary>
/// Encapsulates a distance.
/// For conversions, see
/// http://www.convert-me.com/en/
/// </summary>
public class Distance : SemanticDoubleType<Distance>
{
    /// <summary>
    /// Creates a new distance
    /// </summary>
    /// <param name="value">
    /// Distance in meters
    /// </param>
    public Distance(double value) : base(value) { }

    private const double MilesToMeters = 1609;
    private const double FeetToMeters = 0.3048;
    private const double InchesToMeters = 0.0254;

    /// <summary>
    /// Creates a new distance.
    /// This method is nominally a duplicate of the constructor.
    /// However, using this makes it totally clear that you're passing in a value 
    /// in meters.
    /// </summary>
    /// <param name="value">
    /// Distance in meters
    /// </param>
    /// <returns></returns>
    public static Distance FromMeters(double value) => new(value);

    /// <summary>
    /// Returns the distance in meters.
    /// </summary>
    /// <returns></returns>
    public double Meters
    {
        get { return Value; }
    }

    /// <summary>
    /// Creates a new distance
    /// </summary>
    /// <param name="value">
    /// Distance in miles
    /// </param>
    /// <returns></returns>
    public static Distance FromMiles(double value) => new(value * MilesToMeters);

    /// <summary>
    /// Returns the distance in miles.
    /// </summary>
    /// <returns></returns>
    public double Miles
    {
        get { return Value / MilesToMeters; }
    }

    /// <summary>
    /// Creates a new distance
    /// </summary>
    /// <param name="value">
    /// Distance in feet
    /// </param>
    /// <returns></returns>
    public static Distance FromFeet(double value) => new(value * FeetToMeters);

    /// <summary>
    /// Returns the distance in feet.
    /// </summary>
    /// <returns></returns>
    public double Feet
    {
        get { return Value / FeetToMeters; }
    }

    /// <summary>
    /// Creates a new distance
    /// </summary>
    /// <param name="value">
    /// Distance in inches
    /// </param>
    /// <returns></returns>
    public static Distance FromInches(double value) => new(value * InchesToMeters);

    /// <summary>
    /// Returns the distance in inches.
    /// </summary>
    /// <returns></returns>
    public double Inches
    {
        get { return Value / InchesToMeters; }
    }

    /// <summary>
    /// Creates a new distance, based on combination of feet and inches
    /// </summary>
    /// <returns></returns>
    public static Distance FromImperial(double feet, double inches) => FromInches((12 * feet) + inches);

    /// <summary>
    /// Returns the distance in a combination of feet and inches.
    /// </summary>
    /// <returns></returns>
    public void ToImperial(out double feet, out double inches)
    {
        double totalInches = Inches;
        feet = Math.Floor(totalInches / 12);
        inches = totalInches % 12;
    }

    public static Area operator *(Distance b, Distance c) => (b == null) || (c == null) ? null : new Area(b.Value * c.Value);

}
