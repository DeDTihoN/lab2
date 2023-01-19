<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match = "/Dorm">
		<html>
			<body>
				<table border ="1">
					<tr>
						<th>ПІБ</th>
						<th>Факультет</th>
						<th>Домашня адреса</th>
						<th>Кафедра</th>
						<th>Курс</th>
					</tr>
					<xsl:for-each select="Student">
						<tr>
							<td>
								<xsl:value-of select="Name"/>
							</td>
							<td>
								<xsl:value-of select="Faculty"/>
							</td>
							<td>
								<xsl:value-of select="Adress"/>
							</td>
							<td>
								<xsl:value-of select="Cathedra"/>
							</td>
							<td>
								<xsl:value-of select="CourseYear"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
